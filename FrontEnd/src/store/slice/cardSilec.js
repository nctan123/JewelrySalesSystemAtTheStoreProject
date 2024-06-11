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
        addCustomer: (state, action) => {
            const productIndex = state.CusPoint.findIndex((p) => p.id === action.payload.id)
            if (productIndex === -1) {
                state.CusPoint.push({ ...action.payload, quantity: 1 })
            } else {
                state.CusPoint[productIndex].quantity += 1;
            }
        },
    },

})



// Action creators are generated for each case reducer function
export const { addProduct, deleteProduct, addCustomer } = productSlice.actions

export default productSlice.reducer