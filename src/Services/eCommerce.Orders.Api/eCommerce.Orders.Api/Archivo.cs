namespace eCommerce.Orders.Api
{
    public class Archivo
    {

        public bool metodo(int param) {
            int target = -5;
            int num = 3;

            target = -num;  // Noncompliant; target = -3. Is that really what's meant?
            target = +num;

            switch (param)
            {
                case 0:
                    
                    break;
                default: // default clause should be the first or last one
                   
                    break;
                case 1:
                   
                    break;
            }

            return true;
        }
    }
}
