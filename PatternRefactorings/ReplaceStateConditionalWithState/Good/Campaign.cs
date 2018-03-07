using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternRefactorings.ReplaceStateConditionalWithState.Good
{
    // Finish Up: Move all non-common logic from State base class to subclasses. Try to follow DRY.
    // Delete state transition methods from Context class
    // Replace conditional with one that simply delegates to State field
    // Eventually, expose delegating state transition methods on the context directly and delete UpdateStatus()
    public abstract class CampaignState
    {
        public static CampaignState Cancelled = new CancelledState();
        public static CampaignState Pending = new PendingState();
        public static CampaignState Approved = new ApprovedState();
        public static CampaignState Active = new ActiveState();
        public static CampaignState Completed = new CompletedState();
        private static readonly List<CampaignState> _states = new List<CampaignState>()
        {
            Cancelled,
            Pending,
            Approved,
            Active,
            Completed
        };

        public virtual void MarkCompleted(string userName, Campaign context)
        {
        }

        public virtual void MarkActive(string userName, Campaign context)
        {
        }

        public abstract string Name { get; }
        public static CampaignState ForStatus(string status)
        {
            return _states.Where(s => s.Name == status).FirstOrDefault();
        }

        public virtual void Approve(string userName, Campaign context)
        {
        }

        public void MarkPending(string userName, Campaign context)
        {
            throw new InvalidOperationException("Campaigns are only pending when first created; their status cannot be reset to pending.");
        }

        public virtual void Cancel(string userName, Campaign context)
        {
            // alternately you can leave the working behavior in the base class, if it's common
            string message = String.Format("{0} cancelled {1} campaign {2}", userName, context.State.Name, this.Name);
            AdvertiserNotificationService.NotifyAdvertiser(context.Advertiser, message);
            context.State = CampaignState.Cancelled;
            context.LastUpdatedBy = userName;
        }

    }

    public class CancelledState : CampaignState
    {
        public override string Name
        {
            get { return "cancelled"; }
        }

        public override void Approve(string userName, Campaign context)
        {
            string message = String.Format("{0} approved {1} campaign {2}", userName, context.State.Name, this.Name);
            AdvertiserNotificationService.NotifyAdvertiser(context.Advertiser, message);
            context.State = CampaignState.Approved;
            context.LastUpdatedBy = userName;
        }

        public override void Cancel(string userName, Campaign context)
        {
            throw new InvalidOperationException("Can't cancel a campaign that is already cancelled.");
        }
    }
    public class PendingState : CampaignState
    {
        public override string Name
        {
            get { return "pending"; }
        }

        public override void Approve(string userName, Campaign context)
        {
            string message = String.Format("{0} approved {1} campaign {2}", userName, context.State.Name, this.Name);
            AdvertiserNotificationService.NotifyAdvertiser(context.Advertiser, message);
            context.State = CampaignState.Approved;
            context.LastUpdatedBy = userName;
        }
    }
    public class ApprovedState : CampaignState
    {
        public override string Name
        {
            get { return "approved"; }
        }

        public override void Approve(string userName, Campaign context)
        {
            throw new InvalidOperationException("Can't approve a campaign once it has already been approved.");
        }
    }
    public class ActiveState : CampaignState
    {
        public override string Name
        {
            get { return "active"; }
        }

        public override void Approve(string userName, Campaign context)
        {
            throw new InvalidOperationException("Can't approve a campaign once it is active.");
        }
    }
    public class CompletedState : CampaignState
    {
        public override string Name
        {
            get { return "completed"; }
        }

        public override void Approve(string userName, Campaign context)
        {
            throw new InvalidOperationException("Can't approve a campaign once it has completed.");
        }

        public override void Cancel(string userName, Campaign context)
        {
            throw new InvalidOperationException("Can't cancel a campaign once it has completed.");
        }
    }

    public class Campaign
    {
        public string Name { get; set; }
        public CampaignState State { get; set; }
        public string Advertiser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LastUpdatedBy { get;  set; }

        public Campaign()
        {
            State = CampaignState.Pending;
        }

        // TODO: Update clients to use delegating state transition methods
        public void UpdateStatus(string newStatus, string userName)
        {
            switch (newStatus)
            {
                case "cancelled": 
                    State.Cancel(userName, this);
                    break;
                case "pending": 
                    State.MarkPending(userName, this);
                    break;
                case "approved":
                    State.Approve(userName, this);
                    break;
                case "active":
                    State.MarkActive(userName, this);
                    break;
                case "completed":
                    State.MarkCompleted(userName, this);
                    break;
                default:
                    break;
            }
        }

        // Example delegating method
        public void Cancel(string userName)
        {
            State.Cancel(userName, this);
        }
    }
    public class AdvertiserNotificationService
    {
        public static void NotifyAdvertiser(string advertiser, string message)
        {

        }
    }
}
