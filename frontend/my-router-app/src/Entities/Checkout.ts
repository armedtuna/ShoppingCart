import CheckoutProduct from "./CheckoutProduct";

export default class Checkout {
    public checkoutProducts: CheckoutProduct[]
    public totalPrice: number
    public specialTotalPrice: number
    
    constructor(checkoutProducts: CheckoutProduct[], totalPrice: number, specialTotalPrice: number) {
        this.checkoutProducts = checkoutProducts
        this.totalPrice = totalPrice
        this.specialTotalPrice = specialTotalPrice
    }
}
