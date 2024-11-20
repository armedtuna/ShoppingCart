export default class CheckoutProduct {
    public sku: string
    public unitPrice: number
    public specialPrice?: number

    constructor(sku: string, unitPrice: number, specialPrice?: number) {
        this.sku = sku
        this.unitPrice = unitPrice
        this.specialPrice = specialPrice
    }
}
