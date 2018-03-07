using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternRefactorings.ReplaceDispatcherCommand.Good
{
    // Create command map field
    // Populate the command map in a (static) constructor
    // Modify the conditional to use the map to execute the command
    public class Campaign
    {
        public string Name { get; set; }
        public string Status { get; private set; }
        public string Advertiser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LastUpdatedBy { get; private set; }
        private static Dictionary<string, CampaignCommand> _commandMap;

        public Campaign()
        {
            Status = "pending";
        }

        static Campaign()
        {
            _commandMap = new Dictionary<string, CampaignCommand>();
            _commandMap.Add("cancelled", new CancelCampaignCommand());
            _commandMap.Add("pending", new MarkCampaignPendingCommand());
            _commandMap.Add("approved", new ApproveCampaignCommand());
            _commandMap.Add("active", new ActivateCampaignCommand());
            _commandMap.Add("completed", new MarkCampaignCompleteCommand());
        }

        // This becomes a single line
        public void UpdateStatus(string newStatus, string userName)
        {
            _commandMap[newStatus].Execute(this, userName);
        }

        private abstract class CampaignCommand
        {
            public abstract string NewStatus { get; }
            public abstract void Execute(Campaign campaign, string userName);
        }

        private class CancelCampaignCommand : CampaignCommand
        {
            public override string NewStatus { get { return "cancelled"; } }

            public override void Execute(Campaign campaign, string userName)
            {
                if (campaign.Status == "cancelled")
                {
                    throw new InvalidOperationException("Can't cancel a campaign that is already cancelled.");
                }
                if (campaign.Status == NewStatus)
                {
                    throw new InvalidOperationException("Can't cancel a campaign once it has completed.");
                }
                string message = String.Format("{0} cancelled {1} campaign {2}", userName, campaign.Status, campaign.Name);
                AdvertiserNotificationService.NotifyAdvertiser(campaign.Advertiser, message);
                campaign.Status = NewStatus;
                campaign.LastUpdatedBy = userName;
            }
        }

        private class MarkCampaignPendingCommand : CampaignCommand
        {
            public override string NewStatus { get { return "pending"; } }

            public override void Execute(Campaign campaign, string userName)
            {
                throw new InvalidOperationException("Campaigns are only pending when first created; their status cannot be reset to pending.");
            }
        }

        private class ApproveCampaignCommand : CampaignCommand
        {
            public override string NewStatus { get { return "approved"; } }

            public override void Execute(Campaign campaign, string userName)
            {
                if (campaign.Status == NewStatus)
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
                campaign.Status = NewStatus;
                campaign.LastUpdatedBy = userName;
            }
        }

        private class ActivateCampaignCommand : CampaignCommand
        {
            public override string NewStatus { get { return "active"; } }

            public override void Execute(Campaign campaign, string userName)
            {
                throw new NotImplementedException();
            }
        }

        private class MarkCampaignCompleteCommand : CampaignCommand
        {
            public override string NewStatus { get { return "completed"; } }

            public override void Execute(Campaign campaign, string userName)
            {
                throw new NotImplementedException();
            }
        }

        private void CancelCampaign(string userName)
        {
            var command = new CancelCampaignCommand();
            command.Execute(this, userName);
        }

        private void MarkPending(string userName)
        {
            var command = new MarkCampaignPendingCommand();
            command.Execute(this, userName);
        }


        private void ApproveCampaign(string userName)
        {
            var command = new ApproveCampaignCommand();
            command.Execute(this, userName);
        }

        private void ActivateCampaign(string userName)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        private void MarkComplete(string userName)
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
