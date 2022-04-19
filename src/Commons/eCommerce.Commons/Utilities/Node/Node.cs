
namespace eCommerce.Commons.Utilities.Node
{
    public class Node<T, U> : INode<T, U>
    {
        public T Value { get; set; }
        public string Caption { get; set; }
        public U? Parent { get; set; }
        public override string ToString() => Value.ToString();

        public string path()
        {
            string path = this.ToString();
            while (Parent != null)
            {
                path += $"/{Parent.ToString()}";
            }

            return path;
        }
    }

    public class NodeInt : Node<int, NodeInt>
    {
        public NodeInt(int value, string caption = null, NodeInt? parent = null)
        {
            Value = value;
            Caption = caption;
            Parent = parent;
        }
    }
}
