import { createSlice } from '@reduxjs/toolkit';

export const productSlice = createSlice({
    name: 'products',
    initialState: {
        CartArr: [],
        CusPoint:null,
        CartWholesale: [],
        CartPromotion: [],
    },
    reducers: {
        // Add product to main cart
        addProduct: (state, action) => {
            const productIndex = state.CartArr.findIndex(p => p.id === action.payload.id);
            if (productIndex === -1) {
                state.CartArr.push({ ...action.payload, quantity: 1 });
            } else {
                if (action.payload.categoryId === 6) {
                    state.CartArr[productIndex].quantity += 1;
                }
            }
        },
        deleteProduct: (state, action) => {
            state.CartArr = state.CartArr.filter(item => item.id !== action.payload.id);
        },
        //Add customer
        addCustomer: (state, action) => {
            state.CusPoint = action.payload;
        },
        // Delete customer
        deleteCustomer: (state) => {
            state.CusPoint = null;
        },
        // Clear all products from the main cart
        deleteProductAll: (state) => {
            state.CartArr = [];
        },
        // Add promotion to cart
        addPromotion: (state, action) => {
            const promotionIndex = state.CartPromotion.findIndex(p => p.id === action.payload.id);
            if (promotionIndex === -1) {
                state.CartPromotion.push({ ...action.payload, quantity: 1 });
            }
        },
        deletePromotion: (state, action) => {
            state.CartPromotion = state.CartPromotion.filter(item => item.id !== action.payload.id);
        },
    },
});


export const {
    addCustomer,
    deleteCustomer,
    addProduct,
    deleteProduct,
    deleteProductAll,
    addPromotion,
    deletePromotion,
} = productSlice.actions;

export default productSlice.reducer;
