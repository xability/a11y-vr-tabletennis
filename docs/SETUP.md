# Quick Setup Guide

This guide will help you get the VR Table Tennis project running on your machine quickly.

## Prerequisites

- **Unity 2022.3 LTS** (Download from [Unity Hub](https://unity.com/download))
- **Meta Quest 2/3** or compatible VR headset
- **Git** for version control
- **Visual Studio** or **Rider** for code editing

## Step-by-Step Setup

### 1. Clone the Repository

```bash
git clone https://github.com/xability/a11y-vr-tabletennis.git
cd vr_tt
```

### 2. Open in Unity

1. Launch **Unity Hub**
2. Click **"Open"** → **"Add"**
3. Navigate to the `a11y-vr-tabletennis` folder and select it
4. Choose **Unity 2022.3 LTS** when prompted
5. Wait for Unity to import all assets (this may take several minutes)

### 3. Configure VR Settings

1. Go to **Edit** → **Project Settings**
2. Select **XR Plug-in Management**
3. In the **Installed** tab, check:
   - ✅ **Oculus**
   - ✅ **OpenXR**
4. In the **Android** tab, check:
   - ✅ **Oculus**
   - ✅ **OpenXR**

### 4. Configure Build Settings

1. Go to **File** → **Build Settings**
2. Select **Android** platform
3. Click **"Switch Platform"** (this may take a few minutes)
4. In **Player Settings**:
   - Set **Company Name** and **Product Name**
   - Enable **Virtual Reality Supported**
   - Add **Oculus** to VR SDKs

### 5. Test in Editor

1. Open the main scene: **Assets/Scenes/game scene.unity**
2. Press **Play** in Unity Editor
3. You should see the table tennis environment
4. Use mouse to simulate VR controllers for testing

### 6. Build for VR (Optional)

1. Connect your Meta Quest to your computer
2. Enable **Developer Mode** in Quest settings
3. In Unity, go to **File** → **Build Settings**
4. Click **"Build and Run"**
5. The app will install and launch on your Quest

## Project Structure Overview

```
Assets/
├── Scripts/           # All C# scripts (modular structure)
├── Scenes/           # Unity scenes
├── Prefabs/          # Reusable game objects
├── Materials/         # Visual materials
├── Models/           # 3D models
├── Sounds/           # Audio assets
└── Settings/         # Project configuration
```

## Key Scripts to Know

- **GameManager** (`Core/GameManager.cs`): Central game controller
- **Ball** (`Gameplay/Ball.cs`): Ball physics and collision
- **Tosser** (`Gameplay/Tosser.cs`): Automatic ball tossing
- **AudioGuide** (`Accessibility/AudioGuide.cs`): Audio guidance for BLV users
- **HapticsController** (`Accessibility/HapticsController.cs`): Haptic feedback

## Common Issues and Solutions

### Unity Won't Open Project

**Problem**: Unity shows errors when opening the project
**Solution**: 
1. Make sure you're using Unity 2022.3 LTS
2. Delete the `Library` folder and let Unity regenerate it
3. Check that all packages are properly imported

### VR Headset Not Detected

**Problem**: Unity doesn't recognize your VR headset
**Solution**:
1. Install Oculus/Meta software
2. Enable Developer Mode in Quest settings
3. Check USB connection and drivers
4. Restart Unity and your computer

### Build Errors

**Problem**: Android build fails
**Solution**:
1. Install Android SDK and NDK
2. Set correct Android SDK path in Unity preferences
3. Make sure you have enough disk space
4. Check that all required packages are installed

### Performance Issues

**Problem**: Low frame rate or lag
**Solution**:
1. Reduce graphics quality in Project Settings
2. Optimize lighting and shadows
3. Use object pooling for frequently created objects
4. Profile the application to identify bottlenecks

## Development Workflow

### Making Changes

1. **Create a new branch**:
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes** in Unity
3. **Test thoroughly**:
   - Test in Unity Editor
   - Test on VR hardware if possible
   - Test accessibility features

4. **Commit your changes**:
   ```bash
   git add .
   git commit -m "Add feature description"
   ```

5. **Push and create pull request**:
   ```bash
   git push origin feature/your-feature-name
   ```

### Testing Your Changes

1. **In Unity Editor**:
   - Press Play to test basic functionality
   - Use mouse to simulate VR controllers
   - Check console for errors

2. **On VR Hardware**:
   - Build and deploy to your Quest
   - Test all interaction features
   - Verify accessibility features work

3. **Accessibility Testing**:
   - Test audio cues with headphones
   - Verify haptic feedback (if available)
   - Consider testing with BLV users

## Configuration Tips

### Audio Settings

- Adjust `activationDistance` in AudioGuide for paddle location sensitivity
- Configure audio sources for different collision types
- Set volume levels for optimal accessibility

### Haptic Settings

- Modify `minDistance` and `maxDistance` in HapticsController
- Adjust `minIntensity` and `maxIntensity` for haptic sensitivity
- Configure motor patterns for different feedback types

### Gameplay Settings

- Tune `throwStrength` in Tosser for ball speed
- Adjust `reload` time for ball frequency
- Configure shot positions for varied gameplay

## Getting Help

### Documentation

- **README.md**: Project overview and detailed setup
- **PROJECT_STRUCTURE.md**: Architecture and module descriptions
- **CONTRIBUTING.md**: Development guidelines and standards

### Community

- **GitHub Issues**: Report bugs and request features
- **Discord/Slack**: Real-time discussions (if available)
- **Email**: For sensitive accessibility feedback

### Resources

- **Unity Documentation**: https://docs.unity3d.com/
- **XR Interaction Toolkit**: https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.5/manual/index.html
- **Accessibility Guidelines**: https://www.w3.org/WAI/WCAG21/quickref/

## Next Steps

1. **Explore the codebase**: Read through the scripts to understand the architecture
2. **Test the game**: Play the game to understand the current features
3. **Review accessibility**: Test audio and haptic features
4. **Choose a feature**: Pick an area to contribute to
5. **Start coding**: Follow the contributing guidelines

Welcome to the VR Table Tennis project! We're excited to have you contribute to making VR gaming more accessible for the BLV community.