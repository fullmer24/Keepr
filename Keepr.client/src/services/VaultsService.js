import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {
    async getVaults() {
        const res = await api.get('api/vaults')
        logger.log(res.data, 'getting vaults')
        AppState.vaults = res.data
        logger.log('appstate', AppState.vaults)
    }
}

export const vaultsService = new VaultsService()