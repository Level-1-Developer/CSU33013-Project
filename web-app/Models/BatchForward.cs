using System.Text.Json;
using System.Text.Json.Serialization;

namespace web_app.Models
{
    public class BatchForward
    {
    public Guid ID;
    public int batch_size;
    public String delivery_method;
    public String campaign;
    public DateTime started_at;
    public DateTime completed_at;
    public int total_processed_messages;
    public int total_processed_batches;
    public String forward_status;

    public BatchForward(String ID, int batch_size, String delivery_method, String campaign, String started_at, String completed_at, int total_processed_messages, int total_processed_batches, String forward_status)
    {
        this.ID = Guid.Parse(ID);
        this.batch_size = batch_size;
        this.delivery_method = delivery_method;
        this.campaign = campaign;
        this.started_at = DateTime.Parse(started_at);
        this.completed_at = DateTime.Parse(completed_at);
        this.total_processed_messages = total_processed_messages;
        this.total_processed_batches = total_processed_batches;
        this.forward_status = forward_status;
    }
}

}
