namespace eCommerce.Commons.Utilities.Node
{
    public interface INode<T, U>
    {
        public T Value { get; set; }
        public string Caption { get; set; }
        public U Parent { get; set; }
    }
}
