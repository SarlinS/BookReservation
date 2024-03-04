using System.Text.Json.Serialization;


namespace BookReservation.Models
{
    public class BookItem
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public bool IsReserved { get; set; }
        public string? ReservationComment { get; set; }

        [JsonIgnore]// Navigation property for status history, which is now obsolete
        public List<BookStatusHistory> StatusHistory { get; set; } = new List<BookStatusHistory>();
    }

    public class BookStatusHistory
    {
        public long Id { get; set; }
        public bool IsReserved { get; set; }
        public DateTime ChangeDateTime { get; set; }

        // Foreign key
        public long BookItemId { get; set; }
        [JsonIgnore]  // Exclude this property from serialization to prevent circular reference
        public BookItem? BookItem { get; set; }
    }

}