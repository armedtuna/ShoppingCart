import CheckoutProduct from "./CheckoutProduct";

export default class Checkout {
    public products: CheckoutProduct[]
    public totalPrice: number
    
    constructor(products: CheckoutProduct[], totalPrice: number) {
        this.products = products
        this.totalPrice = totalPrice
    }
}
