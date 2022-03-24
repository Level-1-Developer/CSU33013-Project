class ActionElements
{
    static GetHeadings()
    {
        return ["Id", "Target", "Status", "Campaign", "Time Range",
            "Country", "Language", "Customerset"];
    }

    static GetProperties()
    {
        return ["id", "status", "campaign", "time_range", "country",
            "language", "customerset", "target", "expiry", "when_created",
            "when_updated", "content"
            ];
    }

    static GetElements()
    {
        return [
            {
                Id: "17981491",
                Target: "johndoe@dell.com",
                Status: "converted",
                Campaign: "saved-cart",
                Country: "jp",
                Language: "ja",
                Customerset: "jpbsd1",
                expiry: "2022-01-24 06:00:30",
                when_created: "2022-01-24 04:00:30",
                when_updated: "2022-01-24 06:00:31",
                content: "{\"cart\": {\"id\":  [{\"id\":}]},}"
            },
            {
                Id: "17981492",
                Target: "johndoe@dell.com",
                Status: "purchased",
                Campaign: "abandoned-cart",
                Country: "jp",
                Language: "ja",
                Customerset: "jpdhs1",
                expiry: "2022-01-24 06:00:48",
                when_created: "2022-01-24 04:00:48",
                when_updated: "2022-01-24 04:00:48",
                content: "{\"cart\": {\"id\":  [{\"id\":}]},}"
            },
            {
                Id: "17981493",
                Target: "johndoe@dell.com",
                Status: "converted",
                Campaign: "saved-cart",
                Country: "jp",
                Language: "ja",
                Customerset: "jpbsd1",
                expiry: "2022-01-24 06:00:30",
                when_created: "2022-01-24 04:00:30",
                when_updated: "2022-01-24 06:00:31",
                content: "{\"cart\": {\"id\":  [{\"id\":}]},}"
            },
            {
                Id: "17981494",
                Target: "johndoe@dell.com",
                Status: "converted",
                Campaign: "saved-cart",
                Country: "jp",
                Language: "ja",
                Customerset: "jpbsd1",
                expiry: "2022-01-24 06:00:30",
                when_created: "2022-01-24 04:00:30",
                when_updated: "2022-01-24 06:00:31",
                content: "{\"cart\": {\"id\":  [{\"id\":}]},}"
            },
            {
                Id: "17981495",
                Target: "johndoe@dell.com",
                Status: "converted",
                Campaign: "saved-cart",
                Country: "jp",
                Language: "ja",
                Customerset: "jpbsd1",
                expiry: "2022-01-24 06:00:30",
                when_created: "2022-01-24 04:00:30",
                when_updated: "2022-01-24 06:00:31",
                content: "{\"cart\": {\"id\":  [{\"id\":}]},}"
            }
        ]
    }
}