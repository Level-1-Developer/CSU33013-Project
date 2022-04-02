using System.Text.Json;
using System.Text.Json.Serialization;

namespace web_app.Models
{
    public class ExternalID
    {
        public int ID;
        public int action_items_id;
        public Guid external_id;
        public DateTime created_at;

        public ExternalID(int ID, int action_items_id, String external_id, String created_at)
        {
            this.ID = ID;
            this.action_items_id = action_items_id;
            this.external_id = Guid.Parse(external_id);
            this.created_at = DateTime.Parse(created_at);
        }
    }
}
