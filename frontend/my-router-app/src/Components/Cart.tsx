import * as React from "react";
import Checkout from "../Entities/Checkout";

type CartProps = {
    checkout?: Checkout
}

export default function Cart({
        checkout
    }: CartProps) {

    return (
        <>
            <h1>Cart</h1>

            <ol>
                {checkout?.products?.map((product, index) => {
                    return (
                        <li key={index}>
                            <span>{index} SKU: {product.sku} </span>
                            <span>
                                Price: {product.unitPrice}
                                {product.specialPrice && <span>Special Price: {product.specialPrice}</span>}
                            </span>
                        </li>
                    )
                })}
            </ol>

            <span>Total Price: {checkout?.totalPrice}</span>
        </>
    )
}
