﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BerkeleyEntities;
using BerkeleyEntities.Amazon;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;
using WorkbookPublisher.View;
using BerkeleyEntities.Amazon;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace WorkbookPublisher
{
    
    public class AmznPublisherViewModel
    {
        private berkeleyEntities _dataContext = new berkeleyEntities();
        private List<AmazonEnvelope> _needUserInput = new List<AmazonEnvelope>();
        private Dictionary<AmznListingItem, AmznEntry> _targetListings = new Dictionary<AmznListingItem, AmznEntry>();
        private Publisher _publisher;
        private AmznMarketplace _marketplace;
        private RelayCommand _publish;
        private RelayCommand _fixErrors;

        public AmznPublisherViewModel(int marketplaceID, IEnumerable<AmznEntry> entries)
        {
            this.Entries = new ObservableCollection<AmznEntry>(entries);
            this.CanPublish = true;
            this.CanFixErrors = false;

            _marketplace = _dataContext.AmznMarketplaces.Single(p => p.ID == marketplaceID);
            _publisher = new Publisher(_dataContext, _marketplace);
            _publisher.Error += Publisher_Error;

            UpdateCompetedStatus();
        }

        public string Header { get { return _marketplace.Code; } }

        public ObservableCollection<AmznEntry> Entries { get; set; }

        public bool CanPublish { get; set; }

        public bool CanFixErrors { get; set; }

        public ICommand Publish
        {
            get
            {
                if (_publish == null)
                {
                    _publish = new RelayCommand(PublishAsync);
                }

                return _publish;
            }
        }

        public ICommand FixErrors
        {
            get 
            {
                if (_fixErrors == null)
                {
                    _fixErrors = new RelayCommand(FixErrorsAsync);
                }

                return _fixErrors;
            }
        }

        private async void PublishAsync()
        {
            this.CanPublish = false;

            foreach (AmznEntry entry in this.Entries.Where(p => p.Completed == false))
            {
                AmznListingItem listingItem = _dataContext.AmznListingItems.SingleOrDefault(p => p.IsActive && p.MarketplaceID == _marketplace.ID &&  p.Item.ItemLookupCode.Equals(entry.Sku));

                if (listingItem == null)
                {
                    listingItem = new AmznListingItem();
                    listingItem.Item = _dataContext.Items.Single(p => p.ItemLookupCode.Equals(entry.Sku));
                }

                listingItem.Quantity = entry.Q;
                listingItem.Price = entry.P;    
                listingItem.Condition = entry.Condition;
                listingItem.Title = entry.Title;

                _targetListings.Add(listingItem, entry);
            }

            await Task.Run(() => _publisher.Publish());

            UpdateCompetedStatus();
        }

        private async void FixErrorsAsync()
        {
            var envelopeGroups = _needUserInput.GroupBy(p => p.MessageType).ToList();

            _needUserInput.Clear();

            this.CanFixErrors = false;

            foreach (var group in envelopeGroups)
            {
                var msgs = group.SelectMany(p => p.Message);

                switch (group.Key)
                {
                    case AmazonEnvelopeMessageType.Product :
                        RepublishDataWindow republishForm = new RepublishDataWindow();
                        republishForm.DataContext = CollectionViewSource.GetDefaultView(msgs);
                        republishForm.ShowDialog();
                        await Task.Run( () => _publisher.Republish(group.ToList()));
                        break;

                }
            }
        }

        private void Publisher_Error(ErrorArgs e)
        {
            this.CanFixErrors = true;

            var errors = e.Envelope.Message.Where(p => p.ProcessingResult != null && p.ProcessingResult.ResultCode.Equals(ProcessingReportResultResultCode.Error));

            AmazonEnvelope newEnvelope = new AmazonEnvelope();
            newEnvelope.MessageType = e.Envelope.MessageType;
            newEnvelope.Header = e.Envelope.Header;
            newEnvelope.MarketplaceName = e.Envelope.MarketplaceName;

            List<AmazonEnvelopeMessage> newMsgs = new List<AmazonEnvelopeMessage>();

            int currentMsg = 1;

            foreach(AmazonEnvelopeMessage msg in errors)
            {
                string sku = msg.Item.GetType().GetProperty("SKU").GetValue(msg.Item) as string;

                newMsgs.Add(
                    new AmazonEnvelopeMessage() 
                    { 
                        Item = msg.Item, MessageID = currentMsg.ToString(), OperationType = msg.OperationType, 
                        OperationTypeSpecified = msg.OperationTypeSpecified, ProcessingResult = msg.ProcessingResult
                    });


                AmznEntry entry = _targetListings.Single(p => p.Key.Item.ItemLookupCode.Equals(sku)).Value;

                var existingErrors = entry.Message.Split(new Char[1] { '|' }).ToList();
                existingErrors.Add(msg.ProcessingResult.ResultDescription);

                entry.Message = string.Join("|", existingErrors.Distinct());
                entry.Completed = false;
            }

            newEnvelope.Message = newMsgs.ToArray();

            _needUserInput.Add(newEnvelope);
        }

        private void UpdateCompetedStatus()
        {
            using (berkeleyEntities dataContext = new berkeleyEntities())
            {
                foreach (AmznEntry entry in this.Entries)
                {
                    AmznListingItem listingItem = _dataContext.AmznListingItems.SingleOrDefault(p => p.IsActive && p.MarketplaceID == _marketplace.ID && p.Item.ItemLookupCode.Equals(entry.Sku));

                    if (listingItem != null && listingItem.Quantity == entry.Q)
                    {
                        entry.Completed = true;
                    }
                    else
                    {
                        entry.Completed = false;
                    }
                }
            }

            if (this.Entries.All(p => p.Status.Equals("completed")))
            {
                this.CanPublish = false;
            }
        }

    }

    public class AmznEntry : INotifyPropertyChanged
    {
        private bool _completed;
        private string _message;


        public AmznEntry()
        {
            this.Message = string.Empty;
        }

        public uint RowIndex { get; set; }

        public string Brand { get; set; }
        public string ClassName { get; set; }
        public string Sku { get; set; }
        public int Q { get; set; }
        public decimal P { get; set; }
        public string Title { get; set; }
        public string Condition { get; set; }

        public bool Completed
        {
            get { return _completed; }
            set 
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Completed"));
                }

                _completed = value; 
            }
        }
        public string Message
        {
            get { return _message; }
            set 
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Message")); 
                }
                
                _message = value; 
            }
        }

        public string Status
        {
            get
            {
                if (this.Completed)
                {
                    return "completed";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(this.Message))
                    {
                        return "waiting";
                    }
                    else
                    {
                        return "error";
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
