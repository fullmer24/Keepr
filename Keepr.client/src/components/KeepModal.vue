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
                            <span v-for="k in keep?.views" :key="k"><i class="mdi mdi-eye"></i>{{keep?.views}}</span>
                            <span v-for="k in keep?.kept" :key="k"><i class="mdi mdi-safe"></i>{{keep?.kept}}</span>
                            <div class="text-center">
                                <div class="pt-2">
                                    <h1 class="border-bottom ">{{keep?.name}}</h1>
                                </div>
                                <div class="pt-2">
                                    <h1 class="border-bottom ">{{keep?.description}}</h1>
                                </div>
                                <!-- NOTE add buttons and profile -->
                            </div>
                            <div v-if="account.id == keep?.creatorId">
                                <button @click="deleteKeep"><i class="mdi mdi-delete"></i></button>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <!-- NOTE use for Vault selection -->
                    <!-- <router-link v-if="keep" :to="{name: 'Restaurant Details', params: {id:restaurant?.id}}">
                        <button type="button" data-bs-dismiss="modal" class="btn btn-primary">See Details</button>
                    </router-link> -->
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import { computed } from '@vue/runtime-core';
import { AppState } from '../AppState';
export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),
        };
    },
};
</script>
<style>
/* .modal-img{
        height: 650px;
        width: 600px;
        object-fit: cover;
    } */
</style>