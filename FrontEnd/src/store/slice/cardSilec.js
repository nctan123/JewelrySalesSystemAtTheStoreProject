import { createSlice } from '@reduxjs/toolkit';

export const productSlice = createSlice({
    name: 'products',
    initialState: {
        CartArr: [],
        CartArrTem: [],
        CusPoint: [],
        CusPointTem: [],
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
        // Add product to temporary cart
        addProductTem: (state, action) => {
            const productIndex = state.CartArrTem.findIndex(p => p.id === action.payload.id);
            if (productIndex === -1) {
                state.CartArrTem.push({ ...action.payload, quantity: 1 });
            } else {
                if (action.payload.categoryId === 6) {
                    state.CartArrTem[productIndex].quantity += 1;
                }
            }
        },
        deleteProductTem: (state, action) => {
            state.CartArrTem = state.CartArrTem.filter(item => item.id !== action.payload.id);
        },
        // Clear all products from the main cart
        deleteProductAll: (state) => {
            state.CartArr = [];
        },
        // Add customer to main customer points
        addCustomer: (state, action) => {
            const customerIndex = state.CusPoint.findIndex(p => p.id === action.payload.id);
            if (customerIndex === -1) {
                state.CusPoint.push({ ...action.payload, quantity: 1 });
            }
        },
        deleteCustomer: (state) => {
            state.CusPoint = [];
        },
        // Add customer to temporary customer points
        // addCustomerTem: (state, action) => {
        //     const customerIndex = state.CusPointTem.findIndex(p => p.id === action.payload.id);
        //     if (customerIndex === -1) {
        //         state.CusPointTem.push({ ...action.payload, quantity: 1 });
        //     }
        // },
        // deleteCustomerTem: (state) => {
        //     state.CusPointTem = [];
        // },
        addCustomerTem: (state, action) => {
            state.CusPointTem = action.payload;
          },
          deleteCustomerTem: (state) => {
            state.CusPointTem = [];
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

// Action creators are generated for each case reducer function
export const {
    deleteCustomerTem,
    addCustomerTem,
    addProductTem,
    deleteProductTem,
    addProduct,
    deleteProduct,
    addCustomer,
    deleteCustomer,
    deleteProductAll,
    addPromotion,
    deletePromotion,
} = productSlice.actions;

export default productSlice.reducer;
