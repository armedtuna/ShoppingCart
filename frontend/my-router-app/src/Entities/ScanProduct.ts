// todo-at: is this needed? should it be http result code instead?
export default class ScanResult {
    public sku: string
    
    constructor(sku: string) {
        this.sku = sku
    }
}
