using System.Text.Json;
using System.Text.Json.Serialization;

namespace web_app.Models
{
    public class BatchForwardError
    {
        public Guid ID;
    public String error;
    public Guid batch_forward_id;

    public BatchForwardError(String ID, String error, String batch_forward_id)
    {
        this.ID = Guid.Parse(ID);
        this.error = error;
        this.batch_forward_id = Guid.Parse(batch_forward_id);
    }
}

}
