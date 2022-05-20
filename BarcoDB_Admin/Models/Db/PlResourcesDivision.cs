namespace BarcoDB_Admin.Models.Db
{
    public partial class PlResourcesDivision
    {
        public int Id { get; set; }
        public int ResourcesId { get; set; }
        public string DivisionAfkorting { get; set; } = null!;

        public virtual RqTestDevision DivisionAfkortingNavigation { get; set; } = null!;
        public virtual PlResource Resources { get; set; } = null!;
    }
}
