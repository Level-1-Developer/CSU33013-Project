using System.Text.Json;
using System.Text.Json.Serialization;

namespace web_app.Models
{
    public class Batch
    {
        public Guid ID;
        public Guid batch_forward_id;
        public int size;
        public String status;
        public String campaign;
        public DateTime dispatched_at;
        public Batch(String ID, String batch_forward_id, int size, String status, String campaign, String dispatched_at)
        {
            this.ID = Guid.Parse(ID);
            this.batch_forward_id = Guid.Parse(batch_forward_id);
            this.size = size;
            this.status = status;
            this.campaign = campaign;
            this.dispatched_at = DateTime.Parse(dispatched_at);
        }
    }

}
