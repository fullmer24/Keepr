<template>
    <div class="rounded elevation-4 d-flex justify-content-between selectable" @click="setActive">
        <img class="keep-img" src="keep.img" alt="">
        <div class="p-2">
            <h1>{{keep}}</h1>
        </div>
    </div>
    <KeepModal />
</template>



<script>
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService.js';
import { logger } from '../utils/Logger.js';
import KeepModal from './KeepModal.vue';
export default {
    props: {
        keeps: { type: Object, required: true }
    },
    setup(props) {
        return {
            async setActive() {
                try {
                    Modal.getOrCreateInstance(document.getElementById("keepModal")).toggle();
                    await keepsService.getOne(props.keeps.id)
                } catch (error) {
                    logger.error(error)
                }
            }
        };
    },
    components: { KeepModal }
};
</script>




<style>
.keep-img {
    height: 200px;
    width: 250px;
    object-fit: cover;
}
</style>