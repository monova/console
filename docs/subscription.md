## What do we need?

Subscription must have;
- Start Date
- End Date
- Subscription Features
- User

Monitor;
- SubscriptionId

- Subscription
    - Free
        - 1 monitor
        - 1 monitor step
        - 1 user
        - Minimum interval 5 min (5*60:300)
        - 1 alert channel (email)
    - Startup
        - 5 monitor
        - 10 monitor step
        - 1 user
        - Minumum interval 5 min
        - 2 alert channels (email, slack)
    - Premium
        - 25 monitor
        - 100 monitor step
        - 5 users
        - Minium interval 1 min
        - 5 alert channels (email, slack, telegram, sms, webhook)
    - Enterprise
        - 100 monitor
        - 250 monitor step
        - 25 users
        - Minimum interval 1 min
        - all alert channels (email, slack, telegram, whatsapp, sms, webhook, and additional alert channels)
