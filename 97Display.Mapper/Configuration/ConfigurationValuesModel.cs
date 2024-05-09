using _97Display.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _97Display.Mapper.Configuration
{
    public class ConfigurationValuesModel
    {
        public const string ValuesName = "Values";

        public string AppName { get; set; } = String.Empty;
        public string BuildNumber { get; set; } = String.Empty;
        public string Stage { get; set; } = String.Empty;
        public string WEBSITE_SITE_NAME { get; set; } = String.Empty;
        
        public string AzureWebJobsStorage { get; set; } = String.Empty;
        public string FUNCTIONS_WORKER_RUNTIME { get; set; } = String.Empty;
        public string StorageConnectionString { get; set; } = String.Empty;
        public string DataModelConnectionString { get; set; } = String.Empty;
        public string DataModelContainer { get; set; } = String.Empty;
        public string ServiceBusConnection { get; set; } = String.Empty;
        public string SendgridAPIKey { get; set; } = String.Empty;
        public string IsLive { get; set; } = String.Empty;
        public string WebApiUrl { get; set; } = String.Empty;
        public string WebPassword { get; set; } = String.Empty;
        public string WebServiceUser { get; set; } = String.Empty;
        public string SchedulerApiUrl { get; set; } = String.Empty;
        public string WebhooksEventsKey { get; set; } = String.Empty;
        public string RunEventsPath { get; set; } = String.Empty;
        public string TenMinuteRunPaths { get; set; } = String.Empty;
        public string DailyRunPaths { get; set; } = String.Empty;
        public string finhub_api_baseUrl { get; set; } = String.Empty;
        public string finhub_api_token { get; set; } = String.Empty;
         public string GoogleAPIKey { get; set; } = String.Empty;
        public string GoogleMapsAPIKey2 { get; set; } = String.Empty; 
        public string TokenKey { get; set; } = String.Empty; 
        public string TokenIssuer { get; set; } = String.Empty;


        //public string AttendanceReturnCount { get; set; } = String.Empty;
        //public string SupportFromEmail { get; set; } = String.Empty;
        //public string StatisticsPeriod { get; set; } = String.Empty;
        //public string ResetPasswordValidationSubject { get; set; } = String.Empty;
        //public string ResetPasswordConfirmationSubject { get; set; } = String.Empty;
        //public string RecaptchaPrivateKey { get; set; } = String.Empty;
        //public string RecaptchaPublicKey { get; set; } = String.Empty;
        //public string UploadBaseUrl { get; set; } = String.Empty;
        //public string UploadDocsBaseUrl { get; set; } = String.Empty;
         public string UploadServerUrl { get; set; } = String.Empty;
        //public string ThumbnailUrl { get; set; } = String.Empty;
        //public string UploadVideoServerUrl { get; set; } = String.Empty;
        //public string MaximumCallCount { get; set; } = String.Empty;
        //public string PDTToken { get; set; } = String.Empty;
        public string SMSFrom { get; set; } = String.Empty;
        public string SMSFrom2 { get; set; } = String.Empty;
        public string TwilioCanadaFromNumber { get; set; } = String.Empty;
        //public string AutomaticSMSFrom { get; set; } = String.Empty;
        //public string BaseUrl { get; set; } = String.Empty;
        //public string WebApiUrl { get; set; } = String.Empty;
        //public string MicrosoftServiceBusConnectionString { get; set; } = String.Empty;
        //public string LeadQueueName { get; set; } = String.Empty;
        //public string EmailQueueName { get; set; } = String.Empty;


        //public string CloudName { get; set; } = String.Empty;
        //public string ApiKey { get; set; } = String.Empty;
        //public string ApiSecret { get; set; } = String.Empty;

        //public string RackSpaceDomainURL { get; set; } = String.Empty;
        //public string RackSpaceApiUsername { get; set; } = String.Empty;
        //public string RackSpaceApiKey { get; set; } = String.Empty;

        //public string AquaIISWebsiteName { get; set; } = String.Empty;
        //public string HostingServerIPAddress { get; set; } = String.Empty;
        //public string BindingIPAddress { get; set; } = String.Empty;

        //public string UploadPDFBaseUrl { get; set; } = String.Empty;
        //public string PDFBlobUrl { get; set; } = String.Empty;
        //public string BaseAzureStorageUrl { get; set; } = String.Empty;
        //public string AudioContainerName { get; set; } = String.Empty;
        //public string VideoContainerName { get; set; } = String.Empty;
        //public string PdfFilesContainerName { get; set; } = String.Empty;

        //public string CacheExpirationInMinutes { get; set; } = String.Empty;
        //public string AudioFilesUrl { get; set; } = String.Empty;
        //public string AudioFilesRelativePath { get; set; } = String.Empty;
        //public string IsLive { get; set; } = String.Empty;
        //public string MerchantId { get; set; } = String.Empty;
        //public string PublicKey { get; set; } = String.Empty;
        //public string PrivateKey { get; set; } = String.Empty;
        //public string SMSPotentialLeadFrom { get; set; } = String.Empty;
        //public string AudioIVRRoute0 { get; set; } = String.Empty;

        //public string VideoFilesRelativePath { get; set; } = String.Empty;
        //public string RackspaceUsername { get; set; } = String.Empty;
        //public string RackspacePassword { get; set; } = String.Empty;
        //public string RackspaceContainer { get; set; } = String.Empty;
        //public string AWSBucketName { get; set; } = String.Empty;
        //public string AWSPipelineId { get; set; } = String.Empty;
        //public string AWSAccessKey { get; set; } = String.Empty;
        //public string AWSSecretKey { get; set; } = String.Empty;
        //public string WebServiceUser { get; set; } = String.Empty;
        //public string WebPassword { get; set; } = String.Empty;
        //public string MixPanelAPIKey { get; set; } = String.Empty;
        //public string MixPanelAPISecret { get; set; } = String.Empty;
        //public string ChargifyApiKey { get; set; } = String.Empty;
        //public string ChargifyPassword { get; set; } = String.Empty;
        //public string ChargifyURL { get; set; } = String.Empty;
        //public string ChargifySharedKey { get; set; } = String.Empty;
        //public string ChargifyUseJSON { get; set; } = String.Empty;
        //public string TermsOfServiceVersion { get; set; } = String.Empty;
        //public string StorageConnectionString { get; set; } = String.Empty;
        //public string AweberConsumerKey { get; set; } = String.Empty;
        //public string AweberConsumerSecret { get; set; } = String.Empty;
        //public string FFMPEGPath { get; set; } = String.Empty;
        //public string DelightedKey { get; set; } = String.Empty;
        //public string EnableTracking { get; set; } = String.Empty;
        //public string IncomingCallUrl { get; set; } = String.Empty;
        //public string IncomingSmsUrl { get; set; } = String.Empty;
        //public string GoogleAPIKey { get; set; } = String.Empty;
        //public string GoogleMapsAPIKey2 { get; set; } = String.Empty;
        //public string SendGridAPIKey { get; set; } = String.Empty;

        public string TwilioAccountSID { get; set; } = String.Empty;
        public string TwilioAuthToken { get; set; } = String.Empty;
        public string TwilioApplicationSID { get; set; } = String.Empty;
        public string TwilioPORouletteCallApplicationSID { get; set; } = String.Empty;
        public string TwilioClientCallApplicationSID { get; set; } = String.Empty;

        public string TwilioFromNumber { get; set; } = String.Empty;
        public string TwilioTollFreeNumber { get; set; } = String.Empty;
        public string TwilioLeadCallToNumber { get; set; } = String.Empty;
        public string TwilioPotentialsFromNumber { get; set; } = String.Empty;
        public string TwilioLeadCallApplicationSID { get; set; } = String.Empty;
        public string TwilioUKFromNumber { get; set; } = String.Empty;
        public string TwilioAustraliaFromNumber { get; set; } = String.Empty;
        public string TwilioIrelandFromNumber { get; set; } = String.Empty;
        public string TwilioSouthAfricaFromNumber { get; set; } = String.Empty;
        public string TwilioMalaysiaFromNumber { get; set; } = String.Empty;
        public string TwilioSMSFromNumber { get; set; } = String.Empty;
        public string TwilioBaseUrl { get; set; } = String.Empty;

        //public string InstagramRedirecturl { get; set; } = String.Empty;
        //public string InstagramClientId { get; set; } = String.Empty;
        //public string InstagramGrantType { get; set; } = String.Empty;
        //public string InstagramClientSecret { get; set; } = String.Empty;
        //public string ServiceAPIUrl { get; set; } = String.Empty;
        //public string NewBaseUrl { get; set; } = String.Empty;
        //public string GoogleClientId { get; set; } = String.Empty;
        //public string GoogleClientSecret { get; set; } = String.Empty;
        //public string GoogleAuthCallback { get; set; } = String.Empty;
        //public string GoogleAccountCreateCallback { get; set; } = String.Empty;
        //public string GoogleAppName { get; set; } = String.Empty;

        //public string KendoVersion { get; set; } = String.Empty;
        //public string KendoTheme { get; set; } = String.Empty;

        public bool EnableUserEmailAfterCreatingAccount { get; set; } = false;

        public string InternalApiUrl { get; set; } = String.Empty;
        public string InternalApiPassword { get; set; } = String.Empty; 
        public string InternalApiUser { get; set; } = String.Empty;
        public int Display97OrganizationId { get; set; } = 1;

    }
}
