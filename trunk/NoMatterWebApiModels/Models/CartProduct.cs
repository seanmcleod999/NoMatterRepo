namespace NoMatterWebApiModels.Models
{
	public class CartProduct
	{
		public int CartProductId { get; set; }

		public string CartId { get; set; }

		public Product Product { get; set; }

		public int Quantity { get; set; }

	}
}