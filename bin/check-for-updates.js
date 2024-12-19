const fs = require('fs');
const path = require('path');
const axios = require('axios');
const xml2js = require('xml2js');

// Read the GitHub token from the environment variable
const GITHUB_TOKEN = process.env.GITHUB_TOKEN;
const CHECK_TYPE = process.env.CHECK_TYPE || 'external'; // Default to 'external' if not set

if (!GITHUB_TOKEN) {
  console.error('Error: GitHub token is not set in the environment variables.');
  process.exit(1);
}

async function checkExternalUpdates() {
  const configFilePath = path.join(__dirname, '../Editor/Android/AdGemDependencies.xml');
  const xmlContent = fs.readFileSync(configFilePath, 'utf8');
  const parser = new xml2js.Parser();
  const xml = await parser.parseStringPromise(xmlContent);
  const currentVersion = xml.dependencies.androidPackages[0].androidPackage[0].$.spec.split(':').pop();

  const response = await axios.get('https://api.github.com/repos/AdGem/Android-SDK/releases/latest', {
    headers: {
      'Accept': 'application/vnd.github+json',
      'Authorization': `Bearer ${GITHUB_TOKEN}`,
      'X-GitHub-Api-Version': '2022-11-28'
    }
  });

  let latestVersion = response.data.tag_name;
  latestVersion = latestVersion.replace(/^v/, ''); // Remove leading 'v'

  if (latestVersion !== currentVersion) {
    xml.dependencies.androidPackages[0].androidPackage[0].$.spec = `com.adgem:adgem-android:${latestVersion}`;
    const builder = new xml2js.Builder();
    const updatedXmlContent = builder.buildObject(xml);
    fs.writeFileSync(configFilePath, updatedXmlContent);
  }

  console.log(latestVersion);
}

async function checkOwnRepoUpdates() {
  const packageJsonPath = path.join(__dirname, '../package.json');
  const packageJson = JSON.parse(fs.readFileSync(packageJsonPath, 'utf8'));
  const currentVersion = packageJson.version;

  const response = await axios.get('https://api.github.com/repos/AdGem/adgem-sdk-unity-package/releases/latest', {
    headers: {
      'Accept': 'application/vnd.github+json',
      'Authorization': `Bearer ${GITHUB_TOKEN}`,
      'X-GitHub-Api-Version': '2022-11-28'
    }
  });

  let latestVersion = response.data.tag_name;
  latestVersion = latestVersion.replace(/^v/, ''); // Remove leading 'v'

  if (latestVersion !== currentVersion) {
    packageJson.version = latestVersion;
    fs.writeFileSync(packageJsonPath, JSON.stringify(packageJson, null, 2));
  }

  console.log(latestVersion);
}

console.log('Checking for updates...');
console.log('Github Token:', GITHUB_TOKEN);
console.log('Check Type:', CHECK_TYPE);

if (CHECK_TYPE === 'external') {
  checkExternalUpdates();
} else if (CHECK_TYPE === 'self') {
  checkOwnRepoUpdates();
} else {
  console.error('Error: Invalid check type. Use "external" or "self".'); // We'll add separate checks for android vs ios later
  process.exit(1);
}