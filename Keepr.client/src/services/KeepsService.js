import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {
    async getKeeps() {
        const res = await api.get('api/keeps')
        // logger.log(res.data, 'getting keeps')
        AppState.keeps = res.data
        // logger.log('appstate', AppState.keeps)
    }
    async getOne(id) {
        const res = await api.get(`api/keeps/${id}`)
        // logger.log('get one', res.data)
        AppState.activeKeep = res.data
        // logger.log('active', AppState.activeKeep)
    }
    async deleteKeep(id) {
        const res = await api.delete(`api/keeps/${id}`)
        logger.log(`deleting keep ${keep.name}`, res.data)
        AppState.keeps = AppState.keeps(k => k.id != id)
    }

}


export const keepsService = new KeepsService()