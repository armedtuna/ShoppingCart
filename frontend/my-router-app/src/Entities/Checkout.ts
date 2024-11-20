import CheckoutProduct from "./CheckoutProduct";

export default class Checkout {
    public products: CheckoutProduct[]
    public totalPrice: number
    public specialTotalPrice: number
    
    constructor(products: CheckoutProduct[], totalPrice: number, specialTotalPrice: number) {
        this.products = products
        this.totalPrice = totalPrice
        this.specialTotalPrice = specialTotalPrice
    }
}
