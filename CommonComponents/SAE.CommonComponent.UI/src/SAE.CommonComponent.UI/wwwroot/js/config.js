define(["json!/api/library"], function (librarys) {
    const config = {
        paths: {},
        shim: {}
    };
    librarys.forEach(lib => {
        config.paths[lib.name] = lib.path;
        if (lib.dependencies && lib.dependencies.length > 0) {
            config.shim[lib.name] = lib.dependencies;
            lib.dependencies.forEach((dep, index,array) => {
                if (dep.endsWith(".css")) {
                    array[index] = "css!" + dep.substr(0, dep.length - 4);
                }
            });
        }
    });
    requirejs.config(config);
});