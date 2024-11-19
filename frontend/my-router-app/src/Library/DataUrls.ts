class DataUrlsData {
    cart: string
    products: string
    scan: string
    scanSku: (sku: string) => string

    constructor(baseDataUrl: string) {
        this.cart = `${baseDataUrl}cart`
        this.products = `${baseDataUrl}products`
        this.scan = `${baseDataUrl}scan`
        this.scanSku = (sku) => `${baseDataUrl}scan?sku={sku}`
    }
}

const DataUrls = new DataUrlsData('http://localhost:5044/shoppingcart/')
export default DataUrls
