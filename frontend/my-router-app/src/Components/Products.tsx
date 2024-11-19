import * as React from "react";
import ScanProduct from "../Entities/ScanProduct";
import { postJson } from "../Library/FetchHelper";
import DataUrls from "../Library/DataUrls";
import Product from "../Entities/Product";

type ProductsProps = {
    products?: Product[];
    onScan?: () => void
}

export default function Products({
        products,
        onScan
    }: ProductsProps) {
    const scanProduct = (sku: string) => {
        const scanProduct = new ScanProduct(sku)
        postJson<void>(DataUrls.scan, scanProduct,
            () => {
                if (onScan) {
                    onScan()
                }
            })
    }

    return (
        <>
            <h1>Products</h1>

            <ol>
                {products?.map((product) => {
                    return (
                        <li key={product.sku} onClick={() => scanProduct(product.sku)}>
                            <span>SKU:</span>
                            <span>{product.sku}</span>
                        </li>
                )
                })}
            </ol>
        </>
    )
}
