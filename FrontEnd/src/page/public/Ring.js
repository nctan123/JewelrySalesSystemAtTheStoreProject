import React, { useEffect, useState } from 'react'
import { fetchAllRing } from '../../apis/jewelryService'
import { Header } from '../../components'
const Ring = () => {
  const [listRing, setListRing] = useState([])

  useEffect(() => {
    getRing();
  }, [])

  const getRing = async () => {
    let res = await fetchAllRing();
    if (res && res.data && res.data.data) {
      setListRing(res.data.data)
    }

  }

  return (<>

    <div className='flex p-10 w-full items-center'>

      <div className='grid grid-cols-2 gap-2 w-full overflow-auto'>
        {listRing && listRing.length > 0 &&
          listRing.map((item, index) => {
            return (
              <div key={`ring-${index}`} className='max-w-[100%] rounded overflow-hidden shadow-lg bg-[#99949447]'>
                <div className="px-3 py-3 text-center">
                  <img className='h-[200px] object-cover w-full rounded-lg' src={item.avatar} />
                  <h3>{item.first_name}</h3>
                </div>
              </div>
            )
          })}
      </div>
    </div>

  </>
  )
}

export default Ring