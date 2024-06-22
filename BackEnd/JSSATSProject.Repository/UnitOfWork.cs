using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Repository.Repos;

namespace JSSATSProject.Repository
{
    public class UnitOfWork
    {
        private DBContext _context;
        private AccountRepository _account;
        private BuyOrderRepository _buyOrder;
        private CustomerRepository _customer;
        private DiamondRepository _diamond;
        private DiamondPriceListRepository _diamondpricelist;
        private GuaranteeRepository _guarantee;
        private MaterialPriceListRepository _materialpricelist;
        private MaterialRepository _material;
        private SellOrderRepository _sellOrder;
        private PaymentRepository _payment;
        private PaymentMethodRepository _paymentmethod;
        private PointRepository _point;
        private ProductRepository _product;
        private ProductCategoryRepository _productcategory;
        private ProductCategoryTypeRepository _prodcutcategorytype;
        private PromotionRepository _promotion;
        private ReturnBuyBackPolicyRepository _returnbuybackpolicy;
        private StaffRepository _staff;
        private StallRepository _stall;
        private StallTypeRepository _stalltype;
        private OriginRepository _origin;
        private CutRepository _cut;
        private ColorRepository _color;
        private ClarityRepository _clarity;
        private CaratRepository _carat;
        private FluorescenceRepository _fluorescence;
        private PolishRepository _polish;
        private SymmetryRepository _symmetry;
        private RoleRepository _role;
        private ProductMaterialRepository _productmaterial;
        private SellOrderDetailRepository _sellorderdetail;
        private PromotionRequestRepository _promotionrequest;
        private SpecialDiscountRequestRepository _specialdiscountrequest;
        private PurchasePriceRatioRepository _purchasePriceRatioRepository;
        private PaymentDetailRepository _paymentdetail;

        public UnitOfWork(DBContext context)
        {
            _context = context;
        }
        
        public PurchasePriceRatioRepository PurchasePriceRatioRepository
        {
            get
            {
                if (_purchasePriceRatioRepository == null)
                {
                    _purchasePriceRatioRepository = new PurchasePriceRatioRepository(_context);
                }
                return _purchasePriceRatioRepository;
            }

        }
        
        public SellOrderDetailRepository SellOrderDetailRepository
        {
            get
            {
                if (_sellorderdetail == null)
                {
                    _sellorderdetail = new SellOrderDetailRepository(_context);
                }
                return _sellorderdetail;
            }

        } 
        
        public PaymentDetailRepository PaymentDetailRepository
        {
            get
            {
                if (_paymentdetail == null)
                {
                    _paymentdetail = new PaymentDetailRepository(_context);
                }
                return _paymentdetail;
            }

        } 
        
        public BuyOrderRepository BuyOrderRepository
        {
            get
            {
                if (_buyOrder == null)
                {
                    _buyOrder = new BuyOrderRepository(_context);
                }
                return _buyOrder;
            }

        }

        public ProductMaterialRepository ProductMaterialRepository
        {
            get
            {
                if (_productmaterial == null)
                {
                    _productmaterial = new ProductMaterialRepository(_context);
                }
                return _productmaterial;
            }

        }

        public StallTypeRepository StallTypeRepository
        {
            get
            {
                if (_stalltype == null)
                {
                    _stalltype = new StallTypeRepository(_context);
                }
                return _stalltype;
            }

        }
        public AccountRepository AccountRepository
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_context);
                }
                return _account;
            }
        }

        public CustomerRepository CustomerRepository
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new CustomerRepository(_context);
                }
                return _customer;
            }
        }

        public DiamondRepository DiamondRepository
        {
            get
            {
                if (_diamond == null)
                {
                    _diamond = new DiamondRepository(_context);
                }
                return _diamond;
            }
        }

        public DiamondPriceListRepository DiamondPriceListRepository
        {
            get
            {
                if (_diamondpricelist == null)
                {
                    _diamondpricelist = new DiamondPriceListRepository(_context);
                }
                return _diamondpricelist;
            }
        }

        public GuaranteeRepository GuaranteeRepository
        {
            get
            {
                if (_guarantee == null)
                {
                    _guarantee = new GuaranteeRepository(_context);
                }
                return _guarantee;
            }
        }

        public MaterialPriceListRepository MaterialPriceListRepository
        {
            get
            {
                if (_materialpricelist == null)
                {
                    _materialpricelist = new MaterialPriceListRepository(_context);
                }
                return _materialpricelist;
            }
        }

        public MaterialRepository MaterialRepository
        {
            get
            {
                if (_material == null)
                {
                    _material = new MaterialRepository(_context);
                }
                return _material;
            }
        }

        public SellOrderRepository SellOrderRepository
        {
            get
            {
                if (_sellOrder == null)
                {
                    _sellOrder = new SellOrderRepository(_context);
                }
                return _sellOrder;
            }
        }

        public PaymentRepository PaymentRepository
        {
            get
            {
                if (_payment == null)
                {
                    _payment = new PaymentRepository(_context);
                }
                return _payment;
            }
        }

        public PaymentMethodRepository PaymentMethodRepository
        {
            get
            {
                if (_paymentmethod == null)
                {
                    _paymentmethod = new PaymentMethodRepository(_context);
                }
                return _paymentmethod;
            }
        }

        public PointRepository PointRepository
        {
            get
            {
                if (_point == null)
                {
                    _point = new PointRepository(_context);
                }
                return _point;
            }
        }

        public ProductRepository ProductRepository
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_context);
                }
                return _product;
            }
        }

        public ProductCategoryRepository ProductCategoryRepository
        {
            get
            {
                if (_productcategory == null)
                {
                    _productcategory = new ProductCategoryRepository(_context);
                }
                return _productcategory;
            }
        }

        public ProductCategoryTypeRepository ProductCategoryTypeRepository
        {
            get
            {
                if (_prodcutcategorytype == null)
                {
                    _prodcutcategorytype = new ProductCategoryTypeRepository(_context);
                }
                return _prodcutcategorytype;
            }
        }

        public PromotionRepository PromotionRepository
        {
            get
            {
                if (_promotion == null)
                {
                    _promotion = new PromotionRepository(_context);
                }
                return _promotion;
            }
        }

        public ReturnBuyBackPolicyRepository ReturnBuyBackPolicyRepository
        {
            get
            {
                if (_returnbuybackpolicy == null)
                {
                    _returnbuybackpolicy = new ReturnBuyBackPolicyRepository(_context);
                }
                return _returnbuybackpolicy;
            }
        }

        public StaffRepository StaffRepository
        {
            get
            {
                if (_staff == null)
                {
                    _staff = new StaffRepository(_context);
                }
                return _staff;
            }
        }

        public StallRepository StallRepository
        {
            get
            {
                if (_stall == null)
                {
                    _stall = new StallRepository(_context);
                }
                return _stall;
            }
        }

        public CutRepository CutRepository
        {
            get
            {
                if (_cut == null)
                {
                    _cut = new CutRepository(_context);
                }
                return _cut;
            }
        }

        public ClarityRepository ClarityRepository
        {
            get
            {
                if (_clarity == null)
                {
                    _clarity = new ClarityRepository(_context);
                }
                return _clarity;
            }
        }

        public ColorRepository ColorRepository
        {
            get
            {
                if (_color == null)
                {
                    _color = new ColorRepository(_context);
                }
                return _color;
            }
        }

        public CaratRepository CaratRepository
        {
            get
            {
                if (_carat == null)
                {
                    _carat = new CaratRepository(_context);
                }
                return _carat;
            }
        }

        public PolishRepository PolishRepository
        {
            get
            {
                if (_polish == null)
                {
                    _polish = new PolishRepository(_context);
                }
                return _polish;
            }
        }

        public FluorescenceRepository FluorescenceRepository
        {
            get
            {
                if (_fluorescence == null)
                {
                    _fluorescence = new FluorescenceRepository(_context);
                }
                return _fluorescence;
            }
        }

        public SymmetryRepository SymmetryRepository
        {
            get
            {
                if (_symmetry == null)
                {
                    _symmetry = new SymmetryRepository(_context);
                }
                return _symmetry;
            }
        }

        public OriginRepository OriginRepository
        {
            get
            {
                if (_origin == null)
                {
                    _origin = new OriginRepository(_context);
                }
                return _origin;
            }
        }

        public RoleRepository RoleRepository
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_context);
                }
                return _role;
            }
        }

        public PromotionRequestRepository PromotionRequestRepository
        {
            get
            {
                if (_promotionrequest == null)
                {
                    _promotionrequest = new PromotionRequestRepository(_context);
                }
                return _promotionrequest;
            }
        }

        public SpecialDiscountRequestRepository SpecialDiscountRequestRepository
        {
            get
            {
                if (_specialdiscountrequest == null)
                {
                    _specialdiscountrequest = new SpecialDiscountRequestRepository(_context);
                }
                return _specialdiscountrequest;
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

