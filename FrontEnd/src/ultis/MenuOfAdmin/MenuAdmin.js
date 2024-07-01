import iconAdmin from "./IconAdmin"

const { FaChartSimple,
    TbReportSearch,
    MdManageAccounts,
    CiGift,
    MdOutlineAssignmentReturned,
    TbDeviceIpadCancel,
    RiAccountPinCircleFill,
    BiLogOut, FaChevronDown, IoMdArrowDropdown, IoMdArrowDropup, TbFileInvoice, CgUserList, LiaFileInvoiceDollarSolid, GrUserManager,
    MdOutlineManageAccounts, GiStonePile, LiaGiftsSolid, VscGitPullRequestGoToChanges, GiGoldNuggets, GrDocumentStore } = iconAdmin

export const sidebarMenuAdmin = [
    // {
    //     path: 'revenue',
    //     text:'Revenue',
    //     iconAdmin: <LiaMoneyBillWaveSolid size={24} color="#000055"/>
    // },
    {
        path: 'Dashboard',
        text: 'Dashboard',
        iconAdmin: <FaChartSimple size={24} color="#000055" />
    },
    {
        // path: 'Report',
        text: 'Report',
        iconAdmin: <TbReportSearch size={24} color="#000055" />,
        iconAdmin2: <IoMdArrowDropdown size={24} color="#000055" />,
        iconAdmin3: <IoMdArrowDropup size={24} color="#000055" />,
        subMenu: [
            {
                path: 'Invoice',
                text: 'Invoice',
                iconAdmin: <TbFileInvoice size={24} color="#000055" />
            },
            {
                path: 'ProductSold',
                text: 'Product sold',
                iconAdmin: <LiaFileInvoiceDollarSolid size={24} color="#000055" />
            },
            {
                path: 'Employee',
                text: 'Employee',
                iconAdmin: <CgUserList size={24} color="#000055" />
            },
            {
                path: 'Stall',
                text: 'Stall',
                iconAdmin: <GrDocumentStore size={24} color="#000055" />
            }

        ],

    },
    {
        // path: 'Manage',
        text: 'Manage',
        iconAdmin: <MdManageAccounts size={24} color="#000055" />,
        iconAdmin2: <IoMdArrowDropdown size={24} color="#000055" />,
        iconAdmin3: <IoMdArrowDropup size={24} color="#000055" />,
        subMenu: [
            {
                path: 'productadmin',
                text: 'Product',
                iconAdmin: <GiStonePile size={24} color="#000055" />
            },
            {
                path: 'customeradmin',
                text: 'Customer',
                iconAdmin: <GrUserManager size={24} color="#000055" />
            },
            {
                path: 'point',
                text: 'Point of customer',
                iconAdmin: <GiGoldNuggets size={24} color="#000055" />
            },
            {
                path: 'staff',
                text: 'Staff',
                iconAdmin: <MdOutlineManageAccounts size={24} color="#000055" />
            }

        ],
    },
    {
        // path: 'Promotionadmin',
        text: 'Promotion',
        iconAdmin: <CiGift size={24} color="#000055" />,
        iconAdmin2: <IoMdArrowDropdown size={24} color="#000055" />,
        iconAdmin3: <IoMdArrowDropup size={24} color="#000055" />,
        subMenu: [
            {
                path: 'promotionlist',
                text: 'Promotion list',
                iconAdmin: <LiaGiftsSolid size={24} color="#000055" />
            },
            {
                path: 'promotionrequest',
                text: 'Request',
                iconAdmin: <VscGitPullRequestGoToChanges size={24} color="#000055" />
            }
        ],
    },
    {
        path: 'ReturnPolicy',
        text: 'Return policy',
        iconAdmin: <MdOutlineAssignmentReturned size={24} color="#000055" />
    },

    // {
    //     path: '/login',
    //     text: 'Log out',
    //     iconAdmin: <BiLogOut size={24} color="#000055" />
    // },
]