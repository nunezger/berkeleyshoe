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
using System.Runtime.InteropServices;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.EPS;
using eBay.Service.Util;

#endregion

namespace eBay.Service.Call
{

	/// <summary>
	/// 
	/// </summary>
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ValidateTestUserRegistrationCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public ValidateTestUserRegistrationCall()
		{
			ApiRequest = new ValidateTestUserRegistrationRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public ValidateTestUserRegistrationCall(ApiContext ApiContext)
		{
			ApiRequest = new ValidateTestUserRegistrationRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Requests to enable a test user to sell items in the Sandbox environment.
		/// </summary>
		/// 
		/// <param name="FeedbackScore">
		/// Value for the feedback score of a user. If no value is passed in the request,
		/// or if the value is zero, the feedback score is unchanged. This element is not intended
		/// for regularly testing feedback because the feedback value can change after the request.
		/// </param>
		///
		/// <param name="RegistrationDate">
		/// Value for the date and time that a user's registration begins.
		/// </param>
		///
		/// <param name="SubscribeSA">
		/// Indicates if a user subscribes to Seller's Assistant. You cannot
		/// request to subscribe a user to both Seller's Assistant and
		/// Seller's Assistant Pro. You cannot request to unsubscribe a user.
		/// </param>
		///
		/// <param name="SubscribeSAPro">
		/// Indicates if a user subscribes to Seller's Assistant Pro. You cannot
		/// request to subscribe a user to both Seller's Assistant and
		/// Seller's Assistant Pro. You cannot request to unsubscribe a user.
		/// </param>
		///
		/// <param name="SubscribeSM">
		/// Indicates if a user subscribes to Selling Manager. You cannot
		/// request to subscribe a user to both Selling Manager and
		/// Selling Manager Pro. You cannot request to unsubscribe a user.
		/// </param>
		///
		/// <param name="SubscribeSMPro">
		/// Indicates if a user subscribes to Selling Manager Pro. You cannot
		/// request to subscribe a user to both Selling Manager and
		/// Selling Manager Pro. You cannot request to unsubscribe a user.
		/// </param>
		///
		public void ValidateTestUserRegistration(int FeedbackScore, DateTime RegistrationDate, bool SubscribeSA, bool SubscribeSAPro, bool SubscribeSM, bool SubscribeSMPro)
		{
			this.FeedbackScore = FeedbackScore;
			this.RegistrationDate = RegistrationDate;
			this.SubscribeSA = SubscribeSA;
			this.SubscribeSAPro = SubscribeSAPro;
			this.SubscribeSM = SubscribeSM;
			this.SubscribeSMPro = SubscribeSMPro;

			Execute();
			
		}


		/// <summary>
		/// For backward compatibility with old wrappers.
		/// </summary>
		public void ValidateTestUserRegistration()
		{
			Execute();
		}

		#endregion




		#region Properties
		/// <summary>
		/// The base interface object.
		/// </summary>
		/// <remarks>This property is reserved for users who have difficulty querying multiple interfaces.</remarks>
		public ApiCall ApiCallBase
		{
			get { return this; }
		}

		/// <summary>
		/// Gets or sets the <see cref="ValidateTestUserRegistrationRequestType"/> for this API call.
		/// </summary>
		public ValidateTestUserRegistrationRequestType ApiRequest
		{ 
			get { return (ValidateTestUserRegistrationRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="ValidateTestUserRegistrationResponseType"/> for this API call.
		/// </summary>
		public ValidateTestUserRegistrationResponseType ApiResponse
		{ 
			get { return (ValidateTestUserRegistrationResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="ValidateTestUserRegistrationRequestType.FeedbackScore"/> of type <see cref="int"/>.
		/// </summary>
		public int FeedbackScore
		{ 
			get { return ApiRequest.FeedbackScore; }
			set { ApiRequest.FeedbackScore = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="ValidateTestUserRegistrationRequestType.RegistrationDate"/> of type <see cref="DateTime"/>.
		/// </summary>
		public DateTime RegistrationDate
		{ 
			get { return ApiRequest.RegistrationDate; }
			set { ApiRequest.RegistrationDate = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="ValidateTestUserRegistrationRequestType.SubscribeSA"/> of type <see cref="bool"/>.
		/// </summary>
		public bool SubscribeSA
		{ 
			get { return ApiRequest.SubscribeSA; }
			set { ApiRequest.SubscribeSA = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="ValidateTestUserRegistrationRequestType.SubscribeSAPro"/> of type <see cref="bool"/>.
		/// </summary>
		public bool SubscribeSAPro
		{ 
			get { return ApiRequest.SubscribeSAPro; }
			set { ApiRequest.SubscribeSAPro = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="ValidateTestUserRegistrationRequestType.SubscribeSM"/> of type <see cref="bool"/>.
		/// </summary>
		public bool SubscribeSM
		{ 
			get { return ApiRequest.SubscribeSM; }
			set { ApiRequest.SubscribeSM = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="ValidateTestUserRegistrationRequestType.SubscribeSMPro"/> of type <see cref="bool"/>.
		/// </summary>
		public bool SubscribeSMPro
		{ 
			get { return ApiRequest.SubscribeSMPro; }
			set { ApiRequest.SubscribeSMPro = value; }
		}
		
		

		#endregion

		
	}
}
