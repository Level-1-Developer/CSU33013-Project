using System.Text.Json;
using System.Text.Json.Serialization;

namespace web_app.Models
{
    public class BatchFile
    {
        public Guid ID;
        public Guid batch_id;
        public String file_name;
        public String status;
        public DateTime dispatched_at;
        public String content;

        public BatchFile(String ID, String batch_id, String file_name, String status, String dispatched_at, String content)
        {
            this.ID = Guid.Parse(ID);
            this.batch_id = Guid.Parse(batch_id);
            this.file_name = file_name;
            this.status = status;
            this.dispatched_at = DateTime.Parse(dispatched_at);
            this.content = content;
        }
    }
}
