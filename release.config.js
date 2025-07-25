console.log('Executing Semantic Release configuration...');

const config = {
    branches: ["main", "next"], // or whatever your main branch is named
    plugins: [
        '@semantic-release/commit-analyser',
        '@semantic-release/release-notes-generator',
        ["@semantic-release/git" ,{
            "assets": ["dist/*.js" , "dist/*.js.map"],
            "message": "chore(release): ${nextRelease.version [skip ci] \n\n${nextRelease.notes}}"
        }],
        '@semantic-release/github'
    ]
};

export default config;
