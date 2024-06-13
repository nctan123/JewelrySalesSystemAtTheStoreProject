import { createSlice } from '@reduxjs/toolkit'




export const productSlice = createSlice({

    name: "products",
    initialState: {
        CartArr: [],
        CusPoint: []
    },
    reducers: {
        addProduct: (state, action) => {
            const productIndex = state.CartArr.findIndex((p) => p.id === action.payload.id)
            if (productIndex === -1) {
                state.CartArr.push({ ...action.payload, quantity: 1 })
            } else {
                state.CartArr[productIndex].quantity += 1;
            }
        },
        deleteProduct: (state, action) => {
            const productIndexRemove = action.payload.id
            const newCart = state.CartArr.filter((item) => item.id !== productIndexRemove)
            return { ...state, CartArr: newCart }
        },
        deleteProductAll: (state, action) => {
            return { ...state, CartArr: [] }
        },
        addCustomer: (state, action) => {
            const productIndex = state.CusPoint.findIndex((p) => p.id === action.payload.id)
            if (productIndex === -1) {
                state.CusPoint.push({ ...action.payload, quantity: 1 })
            } else {
                state.CusPoint[productIndex].quantity += 1;
            }
        },
        deleteCustomer: (state, action) => {
            return { ...state, CusPoint: [] }
        },
    },

})



// Action creators are generated for each case reducer function
export const { addProduct, deleteProduct, addCustomer, deleteCustomer, deleteProductAll } = productSlice.actions

export default productSlice.reducer