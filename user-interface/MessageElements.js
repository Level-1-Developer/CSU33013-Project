class MessageElements
{
    static GetHeadings()
    {
        return ["Id", "Status", "Campaign", "Sent"];
    }

    static GetProperties()
    {
        return ["id", "batch_id", "subject", "body", "campaign", "delivery_method",
            "created_at", "sent_at", "status", "action_item_id", "target"
        ];
    }

    static GetElements()
    {
        return [
            {
                Id: "000083b9-b837-4b9a-a7aa-1d310bd273f3",
                batch_id:"bba2265e-c2a4-4fba-87c9-aa20b2b2b3f7",
                subject:"Email|~|First Name|~|Last name|~|Saved Cart ID|~|Item Id|~|Item Type|~|Item title|~|Item Adjusted Total Price|~|Cat 1 Name|~|Cat 2 Name|~|Cat 3 Name|~|ISO Country Code|~|Currency Code|~|Customer Set|~|Language Code|~|Created Time|~|Last Update Time|~|Cart Type|~|Saved Cart Name|~|Marketable flag|~|Product destination|~|Destination order|~|Product type|~|Item image|~|Item URL|~|Rating|~|Number of reviews",
                body: "johndoe@dell.com|~|…",
                Campaign:"saved-cart",
                delivery_method:"BatchDelivery",
                created_at:"2022-03-05 10:58:52",
                Sent:"2022-03-05 17:00:01",
                Status:"Dispatched",
                action_item_id:"18513415",
                target:"johndoe@dell.com",
            },
            {
                Id: "0000987d-fd46-460d-aa5e-c92b556d5d67",
                batch_id:"bef0555b-06eb-4290-8bd6-3883c2af5d0f",
                subject:"Email|~|First Name|~|Last name|~|Abandoned Cart ID|~|Item Id|~|Item Type|~|Item title|~|Item Adjusted Total Price|~|Cat 1 Name|~|Cat 2 Name|~|Cat 3 Name|~|ISO Country Code|~|Currency Code|~|Customer Set|~|Language Code|~|Created Time|~|Last Update Time|~|Cart Type|~|Marketable flag|~|Product destination|~|Destination order|~|Product type|~|Item image|~|Item URL|~|Rating|~|Number of reviews",
                body: "johndoe@dell.com|~|...",
                Campaign:"abandoned-cart",
                delivery_method:"BatchDelivery",
                created_at:"2022-02-17 04:19:15",
                Sent:"2022-02-17 11:10:01",
                Status:"Dispatched",
                action_item_id:"18255817",
                target:"johndoe@dell.com",
            },
            {
                Id: "0000b17f-c49c-4483-9e65-92270218fc5f",
                batch_id:"a9328cb8-b4ff-423d-946c-e7fddf7ba2dc",
                subject:"Email|~|First Name|~|Last name|~|Abandoned Cart ID|~|Item Id|~|Item Type|~|Item title|~|Item Adjusted Total Price|~|Cat 1 Name|~|Cat 2 Name|~|Cat 3 Name|~|ISO Country Code|~|Currency Code|~|Customer Set|~|Language Code|~|Created Time|~|Last Update Time|~|Cart Type|~|Marketable flag|~|Product destination|~|Destination order|~|Product type|~|Item image|~|Item URL|~|Rating|~|Number of reviews",
                body: "johndoe@dell.com|~|...",
                Campaign:"abandoned-cart",
                delivery_method:"BatchDelivery",
                created_at:"2022-03-12 07:48:11",
                Sent:"2022-03-12 14:10:01",
                Status:"Dispatched",
                action_item_id:"18642742",
                target:"johndoe@dell.com",
            },
            {
                Id: "0000df93-dde2-4636-8c15-b7f87f4fba0a",
                batch_id:"ebcf9c4b-a8d9-43a6-8292-db51dccbc9a8",
                subject:"Email|~|First Name|~|Last name|~|Abandoned Cart ID|~|Item Id|~|Item Type|~|Item title|~|Item Adjusted Total Price|~|Cat 1 Name|~|Cat 2 Name|~|Cat 3 Name|~|ISO Country Code|~|Currency Code|~|Customer Set|~|Language Code|~|Created Time|~|Last Update Time|~|Cart Type|~|Marketable flag|~|Product destination|~|Destination order|~|Product type|~|Item image|~|Item URL|~|Rating|~|Number of reviews",
                body: "johndoe@dell.com|~|...",
                Campaign:"abandoned-cart",
                delivery_method:"BatchDelivery",
                created_at:"2022-02-23 04:53:06",
                Sent:"2022-02-23 11:10:01",
                Status:"Dispatched",
                action_item_id:"18357752",
                target:"johndoe@dell.com",
            },
            {
                Id: "0001141e-2253-4ce5-8a0b-c7c14e593d7f",
                batch_id:"9dfe9137-ab59-4f09-865a-0d1c19e20827",
                subject:"Email|~|First Name|~|Last name|~|Saved Cart ID|~|Item Id|~|Item Type|~|Item title|~|Item Adjusted Total Price|~|Cat 1 Name|~|Cat 2 Name|~|Cat 3 Name|~|ISO Country Code|~|Currency Code|~|Customer Set|~|Language Code|~|Created Time|~|Last Update Time|~|Cart Type|~|Saved Cart Name|~|Marketable flag|~|Product destination|~|Destination order|~|Product type|~|Item image|~|Item URL|~|Rating|~|Number of reviews",
                body: "johndoe@dell.com|~|...",
                Campaign:"saved-cart",
                delivery_method:"BatchDelivery",
                created_at:"2022-02-23 00:51:24",
                Sent:"2022-02-23 07:00:01",
                Status:"Dispatched",
                action_item_id:"18355685",
                target:"johndoe@dell.com",
            }
        ]
    }
}