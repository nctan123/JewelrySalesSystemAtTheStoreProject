import React from 'react'

export default function PromotionRequest() {
    return (
<<<<<<< HEAD
        <div>PromotionRequest</div>
    )
}
=======
        <div className="flex items-center justify-center min-h-screen bg-white mx-5 pt-5 mb-5 rounded">
            <div>
                <h1 className="text-3xl font-bold text-center text-blue-800 mb-4 underline"> Promotion Request List</h1>
                <div className="flex mb-4">
                    <div className="relative">
                        <input
                            type="text"
                            placeholder="Search by name"
                            value={searchQuery}
                            onChange={handleSearchChange}
                            className="px-3 py-2 border border-gray-300 rounded-md w-[400px]"
                        />
                        <IoIosSearch className="absolute top-0 right-0 mr-3 mt-3 cursor-pointer text-gray-500" onClick={handleSearch} />
                    </div>
                </div>

                <div className="w-[1200px] overflow-hidden ">
                    <table className="font-inter w-full table-auto border-separate border-spacing-y-1 text-left">
                        <thead className="w-full rounded-lg bg-sky-300 text-base font-semibold text-white sticky top-0">
                            <tr className="whitespace-nowrap text-xl font-bold text-[#212B36] ">
                                <th className="py-3 pl-3 rounded-l-lg">ID</th>
                                <th >Name</th>
                                <th >Description</th>
                                <th className=" text-center">Discount Rate</th>
                                <th >From</th>
                                <th >To</th>
                                <th className="text-center rounded-r-lg">Status</th>
                            </tr>
                        </thead>
                        <tbody>
                            {currentPromotions.map((item, index) => (
                                <tr key={index} className="cursor-pointer font-normal text-[#637381] bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)] text-base hover:shadow-2xl">
                                    <td className="rounded-l-lg pl-3  py-4 text-black">{item.requestId}</td>
                                    <td >{getNamefromDescription(item.description)}</td>
                                    <td>{getDescription(item.description)}</td>
                                    <td className="text-center ">{item.discountRate}</td>
                                    <td >{format(new Date(item.startDate), 'dd/MM/yyyy')}</td>
                                    <td >{format(new Date(item.endDate), 'dd/MM/yyyy')}</td>
                                    <td className=" text-center">
                                        {item.status === 'approved'
                                            ? (<span className="text-green-500 ">Approved</span>)
                                            : item.status === 'rejected' ? (
                                                <span className="text-red-500">Rejected</span>
                                            ) : (<button
                                                className="my-2 border border-white bg-blue-600 text-white rounded-md transition duration-200 ease-in-out hover:bg-[#1d3279] active:bg-[#4741b174] focus:outline-none"
                                                onClick={() => openModal(item.requestId)} // Open modal with requestId on button click
                                            >
                                                {item.status}
                                            </button>)
                                        }

                                    </td>
                                </tr>
                            ))}
                            {placeholders.map((_, index) => (
                                <tr key={`placeholder-${index}`} className="cursor-pointer bg-[#f6f8fa] drop-shadow-[0_0_10px_rgba(34,46,58,0.02)]">
                                    <td className="rounded-l-lg pl-3 text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] text-center py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                    <td className="text-sm font-normal text-[#637381] py-4">-</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>

                <div className="flex justify-center my-4">
                    {Array.from({ length: totalPages }, (_, i) => (
                        <button
                            key={i}
                            onClick={() => handlePageChange(i + 1)}
                            className={clsx(
                                "mx-1 px-3 py-1 rounded",
                                { "bg-blue-500 text-white": currentPage === i + 1 },
                                { "bg-gray-200": currentPage !== i + 1 }
                            )}
                        >
                            {i + 1}
                        </button>
                    ))}
                </div>
            </div>

            {showModal && (
                <StatusUpdateModal
                    onClose={closeModal}
                    requestId={selectedRequestId}
                    handleStatusUpdate={handleStatusUpdate}
                />
            )}
        </div>
    );
};

const StatusUpdateModal = ({ onClose, requestId, handleStatusUpdate }) => {
    const [newStatus, setNewStatus] = useState('approved');

    const handleSubmit = () => {
        handleStatusUpdate(newStatus);
    };

    return (
        <div className="fixed top-0 left-0 w-full h-full bg-gray-800 bg-opacity-50 flex items-center justify-center z-50">
            <div className="bg-white p-4 rounded-lg shadow-lg w-1/3">
                <h2 className="text-lg font-semibold mb-4">Update Promotion Request Status</h2>
                <div className="mb-4">
                    <label htmlFor="status" className="block text-sm font-medium text-gray-700">Response: </label>
                    <select
                        id="status"
                        value={newStatus}
                        onChange={(e) => setNewStatus(e.target.value)}
                        className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                    >
                        <option value="approved">Approved</option>
                        <option value="rejected">Rejected</option>
                    </select>
                </div>
                <div className="flex justify-end">
                    <button
                        onClick={handleSubmit}
                        className="bg-blue-500 text-white px-4 py-2 rounded-md mr-2"
                    >
                        Update
                    </button>
                    <button
                        onClick={onClose}
                        className="bg-gray-300 text-gray-800 px-4 py-2 rounded-md"
                    >
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    );
};

export default PromotionRequest;
>>>>>>> FE_Tai
