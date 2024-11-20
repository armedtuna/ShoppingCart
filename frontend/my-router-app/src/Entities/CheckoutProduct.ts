export default class CheckoutProduct {
    public sku: string
    public unitPrice: number
    public specialPrice?: number
    public hasSpecialPrice?: boolean

    constructor(sku: string, unitPrice: number, specialPrice?: number, hasSpecialPrice?: boolean) {
        this.sku = sku
        this.unitPrice = unitPrice
        this.specialPrice = specialPrice
        this.hasSpecialPrice = hasSpecialPrice
    }
}
