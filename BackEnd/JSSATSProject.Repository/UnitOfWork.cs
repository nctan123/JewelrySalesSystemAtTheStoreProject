using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Repository
{
    public class UnitOfWork
    {
        private DBContext _context;
        private GenericRepository<Account> _account;
        private GenericRepository<Customer> _customer;
        private GenericRepository<Diamond> _diamond;
        private GenericRepository<DiamondPriceList> _diamondpricelist;
        private GenericRepository<Guarantee> _guarantee;
        private GenericRepository<MaterialPriceList> _materialpricelist;
        private GenericRepository<Material> _material;
        private GenericRepository<Order> _order;
        private GenericRepository<Payment> _payment;
        private GenericRepository<PaymentMethod> _paymentmethod;
        private GenericRepository<Point> _Point;
        private GenericRepository<Product> _product;
        private GenericRepository<ProductCategory> _productcategory;
        private GenericRepository<ProductCategoryType> _prodcutcategorytype;
        private GenericRepository<Promotion> _promotion;
        private GenericRepository<ReturnBuyBackPolicy> _returnbuybackpolicy;
        private GenericRepository<Staff> _staff;
        private GenericRepository<Stall> _stall;
        private GenericRepository<StallType> _stalltype;
        private GenericRepository<Origin> _origin;
        private GenericRepository<Cut> _cut;
        private GenericRepository<Color> _color;
        private GenericRepository<Clarity> _clarity;
        private GenericRepository<Carat> _carat;
        private GenericRepository<Fluorescence> _fluorescence;
        private GenericRepository<Polish> _polish;
        private GenericRepository<Symmetry> _symmetry;
        private GenericRepository<Role> _role;



        public UnitOfWork(DBContext context)
        {
            _context = context;
        }

        public GenericRepository<StallType> StallTypeRepository
        {
            get
            {
                if (_stalltype == null)
                {
                    _stalltype = new GenericRepository<StallType>(_context);
                }
                return _stalltype;
            }

        }
        public GenericRepository<Account> AccountRepository
        {
            get
            {
                if (_account == null)
                {
                    _account = new GenericRepository<Account>(_context);
                }
                return _account;
            }
        }

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new GenericRepository<Customer>(_context);
                }
                return _customer;
            }
        }

        public GenericRepository<Diamond> DiamondRepository
        {
            get
            {
                if (_diamond == null)
                {
                    _diamond = new GenericRepository<Diamond>(_context);
                }
                return _diamond;
            }
        }

        public GenericRepository<DiamondPriceList> DiamondPriceListRepository
        {
            get
            {
                if (_diamondpricelist == null)
                {
                    _diamondpricelist = new GenericRepository<DiamondPriceList>(_context);
                }
                return _diamondpricelist;
            }
        }

        public GenericRepository<Guarantee> GuaranteeRepository
        {
            get
            {
                if (_guarantee == null)
                {
                    _guarantee = new GenericRepository<Guarantee>(_context);
                }
                return _guarantee;
            }
        }

        public GenericRepository<MaterialPriceList> MaterialPriceListRepository
        {
            get
            {
                if (_materialpricelist == null)
                {
                    _materialpricelist = new GenericRepository<MaterialPriceList>(_context);
                }
                return _materialpricelist;
            }
        }

        public GenericRepository<Material> MaterialRepository
        {
            get
            {
                if (_material == null)
                {
                    _material = new GenericRepository<Material>(_context);
                }
                return _material;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (_order == null)
                {
                    _order = new GenericRepository<Order>(_context);
                }
                return _order;
            }
        }

        public GenericRepository<Payment> PaymentRepository
        {
            get
            {
                if (_payment == null)
                {
                    _payment = new GenericRepository<Payment>(_context);
                }
                return _payment;
            }
        }

        public GenericRepository<PaymentMethod> PaymentMethodRepository
        {
            get
            {
                if (_paymentmethod == null)
                {
                    _paymentmethod = new GenericRepository<PaymentMethod>(_context);
                }
                return _paymentmethod;
            }
        }

        public GenericRepository<Point> PointRepository
        {
            get
            {
                if (_Point == null)
                {
                    _Point = new GenericRepository<Point>(_context);
                }
                return _Point;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (_product == null)
                {
                    _product = new GenericRepository<Product>(_context);
                }
                return _product;
            }
        }

        public GenericRepository<ProductCategory> ProductCategoryRepository
        {
            get
            {
                if (_productcategory == null)
                {
                    _productcategory = new GenericRepository<ProductCategory>(_context);
                }
                return _productcategory;
            }
        }

        public GenericRepository<ProductCategoryType> ProductCategoryTypeRepository
        {
            get
            {
                if (_prodcutcategorytype == null)
                {
                    _prodcutcategorytype = new GenericRepository<ProductCategoryType>(_context);
                }
                return _prodcutcategorytype;
            }
        }

        public GenericRepository<Promotion> PromotionRepository
        {
            get
            {
                if (_promotion == null)
                {
                    _promotion = new GenericRepository<Promotion>(_context);
                }
                return _promotion;
            }
        }

        public GenericRepository<ReturnBuyBackPolicy> ReturnBuybackPolicyRepository
        {
            get
            {
                if (_returnbuybackpolicy == null)
                {
                    _returnbuybackpolicy = new GenericRepository<ReturnBuyBackPolicy>(_context);
                }
                return _returnbuybackpolicy;
            }
        }

        public GenericRepository<Staff> StaffRepository
        {
            get
            {
                if (_staff == null)
                {
                    _staff = new GenericRepository<Staff>(_context);
                }
                return _staff;
            }
        }

        public GenericRepository<Stall> StallRepository
        {
            get
            {
                if (_stall == null)
                {
                    _stall = new GenericRepository<Stall>(_context);
                }
                return _stall;
            }
        }

        public GenericRepository<Cut> CutRepository
        {
            get
            {
                if (_cut == null)
                {
                    _cut = new GenericRepository<Cut>(_context);
                }
                return _cut;
            }
        }

        public GenericRepository<Clarity> ClariryRepository
        {
            get
            {
                if (_clarity == null)
                {
                    _clarity = new GenericRepository<Clarity>(_context);
                }
                return _clarity;
            }
        }

        public GenericRepository<Color> ColorRepository
        {
            get
            {
                if (_color == null)
                {
                    _color = new GenericRepository<Color>(_context);
                }
                return _color;
            }
        }

        public GenericRepository<Carat> CaratRepository
        {
            get
            {
                if (_carat == null)
                {
                    _carat = new GenericRepository<Carat>(_context);
                }
                return _carat;
            }
        }

        public GenericRepository<Polish> PolishRepository
        {
            get
            {
                if (_polish == null)
                {
                    _polish = new GenericRepository<Polish>(_context);
                }
                return _polish;
            }
        }

        public GenericRepository<Fluorescence> FluorescenceRepository
        {
            get
            {
                if (_fluorescence == null)
                {
                    _fluorescence = new GenericRepository<Fluorescence>(_context);
                }
                return _fluorescence;
            }
        }

        public GenericRepository<Symmetry> SymmetryRepository
        {
            get
            {
                if (_symmetry == null)
                {
                    _symmetry = new GenericRepository<Symmetry>(_context);
                }
                return _symmetry;
            }
        }

        public GenericRepository<Origin> OriginRepository
        {
            get
            {
                if (_origin == null)
                {
                    _origin = new GenericRepository<Origin>(_context);
                }
                return _origin;
            }
        }

        public GenericRepository<Role> RoleRepository
        {
            get
            {
                if (_role == null)
                {
                    _role = new GenericRepository<Role>(_context);
                }
                return _role;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual async ValueTask Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public async ValueTask Dispose()
        {
            await Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

