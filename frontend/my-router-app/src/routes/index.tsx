import * as React from 'react'
import { createFileRoute } from '@tanstack/react-router'
import Cart from "../Components/Cart";
import Products from "../Components/Products";
import Checkout from "../Entities/Checkout";
import { fetchJson } from "../Library/FetchHelper";
import DataUrls from "../Library/DataUrls";
import Product from "../Entities/Product";

export const Route = createFileRoute('/')({
  component: HomeComponent,
})

function HomeComponent() {
    const [checkout, setCheckout] = React.useState<Checkout>()
    const [products, setProducts] = React.useState<Product[]>()
    React.useEffect(() => {
        refreshCheckout()
        refreshProducts()
    }, [])

    const refreshCheckout = () => {
        fetchJson<Checkout>(DataUrls.cart,
            (checkout: Checkout) => {
                setCheckout(checkout)
            })
    }

    const refreshProducts = () => {
        fetchJson<Product[]>(DataUrls.products,
            (products: Product[]) => {
                setProducts(products)
            })
    }

    return (
    <div className="p-2">
        <Cart checkout={checkout} />
        <hr />
        <Products products={products} onScan={() => refreshCheckout()} />
    </div>
  )
}
