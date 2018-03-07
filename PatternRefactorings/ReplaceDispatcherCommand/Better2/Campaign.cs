using System;
using System.Linq;

namespace PatternRefactorings.ReplaceDispatcherCommand.Better2
{
    public class Campaign
    {
        public string Name { get; set; }
        public string Status { get; private set; }
        public string Advertiser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LastUpdatedBy { get; private set; }

        public Campaign()
        {
            Status = "pending";
        }

        // Extract each method into its own concrete class
        public void UpdateStatus(string newStatus, string userName)
        {
            switch (newStatus)
            {
                case "cancelled":
                    CancelCampaign(userName, newStatus);
                    break;
                case "pending":
                    MarkPending(userName, newStatus);
                    break;
                case "approved":
                    ApproveCampaign(userName, newStatus);
                    break;
                case "active":
                    ActivateCampaign(userName, newStatus);
                    break;
                case "completed":
                    MarkComplete(userName, newStatus);
                    break;
                default:
                    break;
            }
        }

        private class CancelCampaignCommand
        {
            public void Execute(Campaign campaign, string userName, string newStatus)
            {
                if (campaign.Status == "cancelled")
                {
                    throw new InvalidOperationException("Can't cancel a campaign that is already cancelled.");
                }
                if (campaign.Status == "completed")
                {
                    throw new InvalidOperationException("Can't cancel a campaign once it has completed.");
                }
                string message = String.Format("{0} cancelled {1} campaign {2}", userName, campaign.Status, campaign.Name);
                AdvertiserNotificationService.NotifyAdvertiser(campaign.Advertiser, message);
                campaign.Status = newStatus;
                campaign.LastUpdatedBy = userName;
            }
        }

        private class MarkCampaignPendingCommand
        {
            public void Execute(Campaign campaign, string userName, string newStatus)
            {
                throw new InvalidOperationException("Campaigns are only pending when first created; their status cannot be reset to pending.");
            }
        }

        private class ApproveCampaignCommand
        {
            public void Execute(Campaign campaign, string userName, string newStatus)
            {
                if (campaign.Status == "approved")
                {
                    throw new InvalidOperationException("Can't approve a campaign once it has already been approved.");
                }
                if (campaign.Status == "completed")
                {
                    throw new InvalidOperationException("Can't approve a campaign once it has completed.");
                }
                if (campaign.Status == "active")
                {
                    throw new InvalidOperationException("Can't approve a campaign once it is active.");
                }
                string message = String.Format("{0} approved {1} campaign {2}", userName, campaign.Status, campaign.Name);
                AdvertiserNotificationService.NotifyAdvertiser(campaign.Advertiser, message);
                campaign.Status = newStatus;
                campaign.LastUpdatedBy = userName;
            }
        }

        private class ActivateCampaignCommand
        {
            public void Execute(Campaign campaign, string userName, string newStatus)
            {
                throw new NotImplementedException();
            }
        }

        private class MarkCampaignCompleteCommand
        {
            public void Execute(Campaign campaign, string userName, string newStatus)
            {
                throw new NotImplementedException();
            }
        }

        private void CancelCampaign(string userName, string newStatus)
        {
            var command = new CancelCampaignCommand();
            command.Execute(this, userName, newStatus);
        }

        private void MarkPending(string userName, string newStatus)
        {
            var command = new MarkCampaignPendingCommand();
            command.Execute(this, userName, newStatus);
        }


        private void ApproveCampaign(string userName, string newStatus)
        {
            var command = new ApproveCampaignCommand();
            command.Execute(this, userName, newStatus);
        }

        private void ActivateCampaign(string userName, string newStatus)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        private void MarkComplete(string userName, string newStatus)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }


    }
    public class AdvertiserNotificationService
    {
        public static void NotifyAdvertiser(string advertiser, string message)
        {

        }
    }
}
