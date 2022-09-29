<template>
    <!-- NOTE add @click to go to vault details page -->
    <div class="row rounded elevation-4 justify-content-center selectable">
        <div class="p-2 col-6 col-md-9">
            <h3 class="border-bottom mt-2 offset-1">{{vault.name}}</h3>
            <h3 class="border-bottom mt-2 offset-1">{{vault.description}}</h3>
        </div>
    </div>
</template>


<script>
export default {
    props: {
        vault: { type: Object, required: true }
    },
    setup() {
        return {};
    },
};
</script>


<style >

</style>