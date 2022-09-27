<template>
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header ">
                    <button title="close" type="button" class="btn-close" data-bs-dismiss="modal"
                        aria-label="Close"></button>
                </div>
                <div class="modal-body ">
                    <div class="row">
                        <div class="col-6">
                            <img class="img-fluid" :src="keep?.img" alt="">
                        </div>
                        <div class="col-6">
                            <span title="views" v-for="k in keep?.views" :key="k"><i
                                    class="mdi mdi-eye"></i>{{keep?.views}}</span>
                            <span v-for="k in keep?.kept" :key="k"><i class="mdi mdi-safe"></i>{{keep?.kept}}</span>
                            <div class="text-center">
                                <div class="pt-2">
                                    <h1 class="">{{keep?.name}}</h1>
                                </div>
                                <div class="pt-2">
                                    <h1 class="border-bottom ">{{keep?.description}}</h1>
                                </div>
                            </div>
                            <div class="row">
                                <!-- NOTE do vault here -->
                                <div title="add to vault" class="col-3 p-2 ms-md-4">
                                    <button>ADD TO VAULT</button>
                                </div>
                                <div title="delete keep" class="col-2 p-2 ms-4 ms-md-5"
                                    v-if="account.id == keep?.creatorId">
                                    <button @click="deleteKeep"><i class="mdi mdi-delete p-s"></i></button>
                                </div>
                                <div title="view profile" class="col-2 p-2 ms-3 ms-md-5">
                                    <!-- NOTE finish profile link -->
                                    <img class="selectable creator-img p-s" :src="keep?.creator.picture" alt="">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <!-- NOTE use for Vault selection -->
                    <!-- <router-link v-if="keep" :to="{name: 'Vault Page', params: {id:keep?.id}}">
                        <button type="button" data-bs-dismiss="modal" class="btn btn-primary">See Details</button>
                    </router-link> -->
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import { keepsService } from '../services/KeepsService.js';
import { computed } from '@vue/runtime-core';
import { AppState } from '../AppState';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { router } from '../router.js';
export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),
            async deleteKeep(keep) {
                try {
                    const yes = await Pop.confirm('Delete this keep?')
                    if (!yes) { return }
                    await keepsService.deleteKeep(keep.id)
                    Pop.toast(`Keep ${keep.name} deleted`)
                    router.push({ name: 'Home' })
                } catch (error) {
                    logger.error('[deleting keep]', error)
                    Pop.error(error)
                }
            }
        };
    },
};
</script>
<style scoped>
.creator-img {
    height: 35px;
    width: 35px;
}
</style>