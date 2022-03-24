class BatchForwardsElements
{
    static GetHeadings()
    {
        return ["id", "campaign", "forward_status", "completed_at"];
    }

    static GetProperties()
    {
        return [ "id", "batch_size", "delivery_method", "campaign", "started_at",
            "completed_at", "total_processed_messages", "total_processed_batches",
            "forward_status"
        ];
    }

    static GetElements()
    {
        return [
            {
                id:"00275822-788e-4f40-8da7-2d7f0ab12040",
                batch_size:"1000",
                delivery_method:"BatchDelivery",
                campaign:"abandoned-cart",
                started_at:"2022-03-10 10:10:00",
                completed_at:"2022-03-10 10:10:01",
                total_processed_messages:"355",
                total_processed_batches:"2",
                forward_status:"Forwarded"
            },
            {
                id:"0057a4d5-022b-4351-a902-211e43140482",
                batch_size:"1000",
                delivery_method:"BatchDelivery",
                campaign:"saved-cart",
                started_at:"2022-03-20 16:00:00",
                completed_at:"2022-03-20 16:00:01",
                total_processed_messages:"34",
                total_processed_batches:"2",
                forward_status:"Forwarded"
            },
            {
                id:"00596c4e-364f-407e-bbcf-af69273e2e81",
                batch_size:"1000",
                delivery_method:"BatchDelivery",
                campaign:"saved-cart",
                started_at:"2022-02-01 17:00:00",
                completed_at:"2022-02-01 17:00:01",
                total_processed_messages:"57",
                total_processed_batches:"2",
                forward_status:"Forwarded"
            },
            {
                id:"006055af-0f5d-4b46-a0d8-0fec96728cf9",
                batch_size:"1000",
                delivery_method:"BatchDelivery",
                campaign:"saved-cart",
                started_at:"2022-03-07 06:00:00",
                completed_at:"2022-03-07 06:00:01",
                total_processed_messages:"20",
                total_processed_batches:"2",
                forward_status:"Forwarded"
            },
            {
                id:"0073a032-5d54-4c87-9e2a-9471abc3979b",
                batch_size:"1000",
                delivery_method:"BatchDelivery",
                campaign:"abandoned-cart",
                started_at:"2022-03-14 09:10:00",
                completed_at:"2022-03-14 09:10:02",
                total_processed_messages:"307",
                total_processed_batches:"2",
                forward_status:"Forwarded"
            }

        ]
    }
}