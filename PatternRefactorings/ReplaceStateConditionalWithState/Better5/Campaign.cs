using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternRefactorings.ReplaceStateConditionalWithState.Better5
{
    // Step 4: For the Approve state transition, override the method from the base class
    // in the state classes that can make a transition to this state
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

        public abstract string Name { get; }

        public static CampaignState ForStatus(string status)
        {
            return _states.Where(s => s.Name == status).FirstOrDefault();
        }

        protected virtual void Approve(string userName, CampaignState newState, Campaign context)
        {
            // Delete body once parts are all moved to subclasses
            // For invalid options, consider leaving exceptions in the base implementation

            //if (context.State == CampaignState.Approved)
            //{
            //    throw new InvalidOperationException("Can't approve a campaign once it has already been approved.");
            //}
            //if (context.State == CampaignState.Completed)
            //{
            //    throw new InvalidOperationException("Can't approve a campaign once it has completed.");
            //}
            //if (context.State == CampaignState.Active)
            //{
            //    throw new InvalidOperationException("Can't approve a campaign once it is active.");
            //}
            //string message = String.Format("{0} approved {1} campaign {2}", userName, context.State.Name, this.Name);
            //AdvertiserNotificationService.NotifyAdvertiser(context.Advertiser, message);
            //context.State = newState;
            //context.LastUpdatedBy = userName;
        }

        private void MarkPending()
        {
            throw new InvalidOperationException("Campaigns are only pending when first created; their status cannot be reset to pending.");
        }

        private void Cancel(string userName, CampaignState newState, Campaign context)
        {
            if (context.State == CampaignState.Cancelled)
            {
                throw new InvalidOperationException("Can't cancel a campaign that is already cancelled.");
            }
            if (context.State == CampaignState.Completed)
            {
                throw new InvalidOperationException("Can't cancel a campaign once it has completed.");
            }
            string message = String.Format("{0} cancelled {1} campaign {2}", userName, context.State.Name, this.Name);
            AdvertiserNotificationService.NotifyAdvertiser(context.Advertiser, message);
            context.State = newState;
            context.LastUpdatedBy = userName;
        }
    }

    public class CancelledState : CampaignState
    {
        public override string Name
        {
            get { return "cancelled"; }
        }

        protected override void Approve(string userName, CampaignState newState, Campaign context)
        {
            string message = String.Format("{0} approved {1} campaign {2}", userName, context.State.Name, this.Name);
            AdvertiserNotificationService.NotifyAdvertiser(context.Advertiser, message);
            context.State = newState;
            context.LastUpdatedBy = userName;
        }
    }
    public class PendingState : CampaignState
    {
        public override string Name
        {
            get { return "pending"; }
        }

        protected override void Approve(string userName, CampaignState newState, Campaign context)
        {
            string message = String.Format("{0} approved {1} campaign {2}", userName, context.State.Name, this.Name);
            AdvertiserNotificationService.NotifyAdvertiser(context.Advertiser, message);
            context.State = newState;
            context.LastUpdatedBy = userName;
        }
    }
    public class ApprovedState : CampaignState
    {
        public override string Name
        {
            get { return "approved"; }
        }

        protected override void Approve(string userName, CampaignState newState, Campaign context)
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

        protected override void Approve(string userName, CampaignState newState, Campaign context)
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

        protected override void Approve(string userName, CampaignState newState, Campaign context)
        {
            throw new InvalidOperationException("Can't approve a campaign once it has completed.");
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

        public void UpdateStatus(string newStatus, string userName)
        {
            CampaignState newState = CampaignState.ForStatus(newStatus);
            if(newState == CampaignState.Cancelled)
            {
                Cancel(userName, newState);
            }
            else if(newState==CampaignState.Pending)
            {
                MarkPending();
            }
            else if (newState==CampaignState.Approved)
            {
                Approve(userName, newState);
            }
            else if (newState==CampaignState.Active)
            {}
            else if (newState==CampaignState.Completed)
            {}
        }

        private void Approve(string userName, CampaignState newState)
        {
            if (this.State == CampaignState.Approved)
            {
                throw new InvalidOperationException("Can't approve a campaign once it has already been approved.");
            }
            if (this.State == CampaignState.Completed)
            {
                throw new InvalidOperationException("Can't approve a campaign once it has completed.");
            }
            if (this.State == CampaignState.Active)
            {
                throw new InvalidOperationException("Can't approve a campaign once it is active.");
            }
            string message = String.Format("{0} approved {1} campaign {2}", userName, this.State.Name, this.Name);
            AdvertiserNotificationService.NotifyAdvertiser(this.Advertiser, message);
            this.State = newState;
            this.LastUpdatedBy = userName;
        }
  
        private void MarkPending()
        {
            throw new InvalidOperationException("Campaigns are only pending when first created; their status cannot be reset to pending.");
        }
  
        private void Cancel(string userName, CampaignState newState)
        {
            if (this.State == CampaignState.Cancelled)
            {
                throw new InvalidOperationException("Can't cancel a campaign that is already cancelled.");
            }
            if (this.State == CampaignState.Completed)
            {
                throw new InvalidOperationException("Can't cancel a campaign once it has completed.");
            }
            string message = String.Format("{0} cancelled {1} campaign {2}", userName, this.State.Name, this.Name);
            AdvertiserNotificationService.NotifyAdvertiser(this.Advertiser, message);
            this.State = newState;
            this.LastUpdatedBy = userName;
        }

    }
    public class AdvertiserNotificationService
    {
        public static void NotifyAdvertiser(string advertiser, string message)
        {

        }
    }
}
