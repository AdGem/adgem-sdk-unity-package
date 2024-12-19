const axios = require('axios');

// Read the GitHub token from the environment variable
const GITHUB_TOKEN = process.env.GITHUB_TOKEN;
const CHECK_TYPE = process.env.CHECK_TYPE || 'external'; // Default to 'external' if not set

if (!GITHUB_TOKEN) {
  console.error('Error: GitHub token is not set in the environment variables.');
  process.exit(1);
}

async function checkExternalUpdates() {
  const response = await axios.get('https://api.github.com/repos/AdGem/Android-SDK/releases/latest', {
    headers: {
      'Accept': 'application/vnd.github+json',
      'Authorization': `Bearer ${GITHUB_TOKEN}`,
      'X-GitHub-Api-Version': '2022-11-28'
    }
  });

  let latestVersion = response.data.tag_name;
  latestVersion = latestVersion.replace(/^v/, ''); // Remove leading 'v'

  // if (latestVersion !== currentVersion) {
  //   xml.dependencies.androidPackages[0].androidPackage[0].$.spec = `com.adgem:adgem-android:${latestVersion}`;
  //   const builder = new xml2js.Builder();
  //   const updatedXmlContent = builder.buildObject(xml);
  //   fs.writeFileSync(configFilePath, updatedXmlContent);
  // }

  console.log(latestVersion);
}

async function checkOwnRepoUpdates() {
  const repoName = process.env.REPO_NAME || 'adgem-sdk-unity-package';

  try {
    const response = await axios.get(`https://api.github.com/repos/AdGem/${repoName}/releases/latest`, {
      headers: {
        'Accept': 'application/vnd.github+json',
        'Authorization': `Bearer ${GITHUB_TOKEN}`,
        'X-GitHub-Api-Version': '2022-11-28'
      }
    });

    let latestReleaseVersion = response.data.tag_name.replace(/^v/, '');

    console.log(latestReleaseVersion);
  } catch (error) {
    console.error('Error:', error.message);
    process.exit(1);
  }
}

if (CHECK_TYPE === 'external') {
  checkExternalUpdates();
} else if (CHECK_TYPE === 'self') {
  checkOwnRepoUpdates();
} else {
  console.error('Error: Invalid check type. Use "external" or "self".'); // We'll add separate checks for android vs ios later
  process.exit(1);
}