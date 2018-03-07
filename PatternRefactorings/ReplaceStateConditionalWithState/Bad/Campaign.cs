using System;
using System.Linq;

namespace PatternRefactorings.ReplaceStateConditionalWithState.Bad
{
    /// <summary>
    /// We're going to use State here, but you can also use Command for this kind of scenario
    /// </summary>
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

        public void UpdateStatus(string newStatus, string userName)
        {
            switch (newStatus)
            {
                case "cancelled":
                    {
                        if (this.Status == "cancelled")
                        {
                            throw new InvalidOperationException("Can't cancel a campaign that is already cancelled.");
                        }
                        if (this.Status == "completed")
                        {
                            throw new InvalidOperationException("Can't cancel a campaign once it has completed.");
                        }
                        string message = String.Format("{0} cancelled {1} campaign {2}", userName, this.Status, this.Name);
                        AdvertiserNotificationService.NotifyAdvertiser(this.Advertiser, message);
                        this.Status = newStatus;
                        this.LastUpdatedBy = userName;
                        break;
                    }
                case "pending":
                    throw new InvalidOperationException("Campaigns are only pending when first created; their status cannot be reset to pending.");
                    break;
                case "approved":
                    {
                        if (this.Status == "approved")
                        {
                            throw new InvalidOperationException("Can't approve a campaign once it has already been approved.");
                        }
                        if (this.Status == "completed")
                        {
                            throw new InvalidOperationException("Can't approve a campaign once it has completed.");
                        }
                        if (this.Status == "active")
                        {
                            throw new InvalidOperationException("Can't approve a campaign once it is active.");
                        }
                        string message = String.Format("{0} approved {1} campaign {2}", userName, this.Status, this.Name);
                        AdvertiserNotificationService.NotifyAdvertiser(this.Advertiser, message);
                        this.Status = newStatus;
                        this.LastUpdatedBy = userName;
                        break;
                    }
                case "active":
                    // more complex logic
                    break;
                case "completed":
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
