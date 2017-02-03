using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using RestaurantBilling.Core.ViewModels;
using UIKit;


namespace RestaurantBilling.UI.iOS.Views
{
    public partial class BillView : MvxViewController<BillViewModel>
    {
        public BillView() : base("BillView", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // This establishes the same sort of data binding in code as what we did in Android's axml.
            // It means: Bind the VCEmail's default binding property (Text in this case) 
            // to the ViewModel's CustomerEmail property.
            // 2-way binding by default.
            this.CreateBinding(VCEmail).To((BillViewModel vm) => vm.CustomerEmail).Apply();

            // This is a similar example, but demonstrates binding to a specific property (Text).
            this.CreateBinding(VCSubTotal).For(label => label.Text).To((BillViewModel vm) => vm.SubTotal).Apply();

            this.CreateBinding(VCGratuity).To((BillViewModel vm) => vm.Gratuity).Apply();
            this.CreateBinding(VCTip).To((BillViewModel vm) => vm.Tip).Apply();
            this.CreateBinding(VCTotal).To((BillViewModel vm) => vm.Total).Apply();

            // This ensures that the virtual keyboard is closed after text input.
            View.AddGestureRecognizer(new UITapGestureRecognizer(() => {
                this.VCEmail.ResignFirstResponder();
                this.VCSubTotal.ResignFirstResponder();
            }));
        }
    }
}