import * as React from "react";
import Checkout from "../Entities/Checkout";
import { createColumnHelper, flexRender, getCoreRowModel, useReactTable } from '@tanstack/react-table'
import CheckoutProduct from "../Entities/CheckoutProduct";

import './cart.css'

type CartProps = {
    checkout?: Checkout
}

export default function Cart({
        checkout
    }: CartProps) {
    const columnHelper = createColumnHelper<CheckoutProduct>()
    const columns = [
        columnHelper.accessor(row => row.sku, {
            id: 'sku',
            cell: info => info.getValue(),
            header: () => <span>Product SKU</span>,
            footer: () => <span>Totals</span>,
        }),
        columnHelper.accessor(row => row.unitPrice, {
            id: 'unitPrice',
            cell: info => info.getValue(),
            header: () => <span>Unit Price</span>,
            footer: () => <span>{checkout?.totalPrice}</span>,
        }),
        columnHelper.accessor(row => row.specialPrice, {
            id: 'specialPrice',
            cell: info => info.getValue(),
            header: () => <span>Special Price</span>,
            footer: () => <span>{checkout?.specialTotalPrice}</span>,
        })
    ]
    const data = checkout?.products ?? []
    const table = useReactTable({ data, columns, getCoreRowModel: getCoreRowModel() })

    return (
        <>
            <h1>Cart</h1>

            <table>
                <thead>
                {table.getHeaderGroups().map(headerGroup => (
                    <tr key={headerGroup.id}>
                        {headerGroup.headers.map((header) => (
                            <th key={header.id}>
                                {header.isPlaceholder
                                    ? null
                                    : flexRender(
                                        header.column.columnDef.header,
                                        header.getContext()
                                    )}
                            </th>
                        ))}
                    </tr>
                ))}
                </thead>
                <tbody>
                {table.getRowModel().rows.map((row) => (
                    <tr key={row.id}>
                        {row.getVisibleCells().map((cell) => (
                            <td key={cell.id}>
                                {flexRender(cell.column.columnDef.cell, cell.getContext())}
                            </td>
                        ))}
                    </tr>
                ))}
                </tbody>
                <tfoot>
                {table.getFooterGroups().map(footerGroup => (
                    <tr key={footerGroup.id}>
                        {footerGroup.headers.map((header) => (
                            <th key={header.id}>
                                {header.isPlaceholder
                                    ? null
                                    : flexRender(
                                        header.column.columnDef.footer,
                                        header.getContext()
                                    )}
                            </th>
                        ))}
                    </tr>
                ))}
                </tfoot>
            </table>
        </>
    )
}
