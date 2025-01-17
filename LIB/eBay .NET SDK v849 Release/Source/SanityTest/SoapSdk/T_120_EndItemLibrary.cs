#region Copyright
//	Copyright (c) 2013 eBay, Inc.
//	
//	This program is licensed under the terms of the eBay Common Development and
//	Distribution License (CDDL) Version 1.0 (the "License") and any subsequent  
//	version thereof released by eBay.  The then-current version of the License can be 
//	found at http://www.opensource.org/licenses/cddl1.php and in the eBaySDKLicense 
//	file that is under the eBay SDK ../docs directory
#endregion

#region Namespaces
using System;
using NUnit.Framework;
using eBay.Service.Call;
using eBay.Service.Core.Soap;
using eBay.Service.Core.Sdk;
using eBay.Service.Util;
using UnitTests.Helper;
#endregion

namespace AllTestsSuite.T_050_ItemTestsSuite
{
	[TestFixture]
	public class T_120_EndItemLibrary : SOAPTestBase
	{
		[Test]
		public void EndItem()
		{
			Assert.IsNotNull(TestData.NewItem, "Failed because no item available -- requires successful AddItem test");
			
			ItemType item = TestData.NewItem;
			EndItemCall api = new EndItemCall(this.apiContext);
			// Set the item to be ended.
			api.ItemID = item.ItemID;
			api.EndingReason = EndReasonCodeType.NotAvailable;
			api.Execute();
			
			//check whether the call is success.
			Assert.IsTrue(api.ApiResponse.Ack==AckCodeType.Success || api.ApiResponse.Ack==AckCodeType.Warning,"do not success!");
			TestData.EndedItem = TestData.NewItem;

			Assert.IsNotNull(TestData.EndedItem);
		}

		[Test]
		public void EndFixedPriceItem()
		{
			Assert.IsNotNull(TestData.NewFixedPriceItem, "Failed because no item available -- requires successful AddFixedPriceItem test");
			
			ItemType item = TestData.NewFixedPriceItem;
			EndFixedPriceItemCall api = new EndFixedPriceItemCall(this.apiContext);
			// Set the item to be ended.
			api.ItemID = item.ItemID;
			api.EndingReason = EndReasonCodeType.NotAvailable;
			api.Execute();
			
			//check whether the call is success.
			Assert.IsTrue(api.ApiResponse.Ack==AckCodeType.Success || api.ApiResponse.Ack==AckCodeType.Warning,"do not success!");
			TestData.EndedFixedPriceItem = TestData.NewFixedPriceItem;

			Assert.IsNotNull(TestData.EndedFixedPriceItem);
		}

		[Test]
		public void EndItemFull()
		{
			Assert.IsNotNull(TestData.NewItem2, "Failed because no item available -- requires successful AddItem test");
			
			ItemType item = TestData.NewItem2;
			EndItemCall api = new EndItemCall(this.apiContext);
			// Set the item to be ended.
			api.ItemID = item.ItemID;
			api.EndingReason = EndReasonCodeType.NotAvailable;
			api.Execute();
			
			//check whether the call is success.
			Assert.IsTrue(api.ApiResponse.Ack==AckCodeType.Success || api.ApiResponse.Ack==AckCodeType.Warning,"do not success!");
			TestData.EndedItem2 = TestData.NewItem2;
			
			Assert.IsNotNull(TestData.EndedItem2);
			//Assert.IsTrue(false,"NewItem:"+TestData.NewItem.ItemID+";EndedItem:"+TestData.EndedItem.ItemID+";NewItem2:"+TestData.NewItem2.ItemID+";EndedItem2:"+TestData.EndedItem2.ItemID);
		}


		public void EndChineseAuctionItem()
		{
			Assert.IsNotNull(TestData.ChineseAuctionItem, "Failed because no item available -- requires successful AddItem test");
			
			ItemType item = TestData.ChineseAuctionItem;
			EndItemCall api = new EndItemCall(this.apiContext);
			// Set the item to be ended.
			api.ItemID = item.ItemID;
			api.EndingReason = EndReasonCodeType.NotAvailable;
			api.Execute();
			
			//check whether the call is success.
			Assert.IsTrue(api.ApiResponse.Ack==AckCodeType.Success||api.ApiResponse.Ack==AckCodeType.Warning,"do not success!");
		}

	}
}