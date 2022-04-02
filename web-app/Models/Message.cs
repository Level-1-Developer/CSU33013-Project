using System.Text.Json;
using System.Text.Json.Serialization;

namespace web_app.Models
{
    public class Message
    {
        public Guid ID;
        public Guid batch_id;
        public String subject;
        public String body;
        public String campaign;
        public String delivery_method;
        public DateTime created_at;
        public DateTime sent_at;
        public String status;
        public int action_item_id;
        public String target;
        public Message(String ID, String batch_id, String subject, String body, String campaign, String delivery_method, String created_at, String sent_at, String status, int action_item_id, String target)
        {
            this.ID = Guid.Parse(ID);
            this.batch_id = Guid.Parse(batch_id);
            this.subject = subject;
            this.body = body;
            this.campaign = campaign;
            this.delivery_method = delivery_method;
            this.created_at = DateTime.Parse(created_at);
            this.sent_at = DateTime.Parse(sent_at);
            this.status = status;
            this.action_item_id = action_item_id;
            this.target = target;
        }
    }

}
