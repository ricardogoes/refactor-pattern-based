using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternRefactorings.ReplaceStateConditionalWithState.Better1
{
    // Step 1: Replace Type Code with Class
    //  Set up context class to use constants instead of just strings
    public class CampaignState
    {
        public static CampaignState Cancelled = new CampaignState("cancelled");
        public static CampaignState Pending = new CampaignState("pending");
        public static CampaignState Approved = new CampaignState("approved");
        public static CampaignState Active = new CampaignState("active");
        public static CampaignState Completed = new CampaignState("completed");
        private static readonly List<CampaignState> _states = new List<CampaignState>()
        {
            Cancelled,
            Pending,
            Approved,
            Active,
            Completed
        };

        public CampaignState(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public static CampaignState ForStatus(string status)
        {
            return _states.Where(s => s.Name == status).FirstOrDefault();
        }
    }

    public class Campaign
    {
        public const string PENDING = "pending";
        public const string CANCELLED = "cancelled";
        public const string APPROVED = "approved";
        public const string ACTIVE = "active";
        public const string COMPLETED = "completed";

        public string Name { get; set; }
        public string Status { get; private set; }
        public string Advertiser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LastUpdatedBy { get; private set; }

        public Campaign()
        {
            Status = PENDING;
        }

        public void UpdateStatus(string newStatus, string userName)
        {
            switch (newStatus)
            {
                case CANCELLED:
                    {
                        if (this.Status == CANCELLED)
                        {
                            throw new InvalidOperationException("Can't cancel a campaign that is already cancelled.");
                        }
                        if (this.Status == COMPLETED)
                        {
                            throw new InvalidOperationException("Can't cancel a campaign once it has completed.");
                        }
                        string message = String.Format("{0} cancelled {1} campaign {2}", userName, this.Status, this.Name);
                        AdvertiserNotificationService.NotifyAdvertiser(this.Advertiser, message);
                        this.Status = newStatus;
                        this.LastUpdatedBy = userName;
                        break;
                    }
                case PENDING:
                    throw new InvalidOperationException("Campaigns are only pending when first created; their status cannot be reset to pending.");
                    break;
                case APPROVED:
                    {
                        if (this.Status == APPROVED)
                        {
                            throw new InvalidOperationException("Can't approve a campaign once it has already been approved.");
                        }
                        if (this.Status == COMPLETED)
                        {
                            throw new InvalidOperationException("Can't approve a campaign once it has completed.");
                        }
                        if (this.Status == ACTIVE)
                        {
                            throw new InvalidOperationException("Can't approve a campaign once it is active.");
                        }
                        string message = String.Format("{0} approved {1} campaign {2}", userName, this.Status, this.Name);
                        AdvertiserNotificationService.NotifyAdvertiser(this.Advertiser, message);
                        this.Status = newStatus;
                        this.LastUpdatedBy = userName;
                        break;
                    }
                case ACTIVE:
                    // more complex logic
                    break;
                case COMPLETED:
                    // more complex logic
                    break;
                default:
                    break;
            }

        }

    }
    public class AdvertiserNotificationService
    {
        public static void NotifyAdvertiser(string advertiser, string message)
        {

        }
    }
}
