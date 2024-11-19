export default class Product {
    public sku: string
    public unitPrice: number
    
    constructor(sku: string, unitPrice: number) {
        this.sku = sku
        this.unitPrice = unitPrice
    }
}
